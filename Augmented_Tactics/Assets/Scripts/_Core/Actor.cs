﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;


/*********************************
The Actor class is the parent class
of all battlefield objects. So this means
that the Actor is the parent for
BOTH PLAYERS AND ENEMYS on the field.
THIS IS NOT JUST THE PLAYER CLASS
PLAYER CLASS IS A CHILD OF THIS CLASS
CALLED PlayerControlled
*************************************/


public class Actor : MonoBehaviour
{
    /******************
     *  Variables
     ******************/

    #region Variables

    protected Animator anim;

    public float health_current;    // temporary for debugging purposes(should be protected)
    protected float health_max; 
    protected float mana_current;
    protected float mana_max;
    protected float move_speed;
    protected float armor_class;
    protected int counterAttack;

    //stats
    protected int strength;         //measuring physical power
    protected int dexterity;        //measuring agility
    protected int constitution;     //measuring endurance
    protected int intelligence;     //measuring reasoning and memory (Magic Damage)
    protected int wisdom;           //measuring perception and insight (Resistance/Healing)
    protected int charisma;         //measuring force of personality (Buffs and Debuffs)

    public Ability[] abilitySet;
    private int experience;

    //Movement 
    public TileMap map;
    public Vector3 coords;
    private List<Nodes> currentPath = null;
    NavMeshAgent playerAgent;
    public float speed;
    public int moveDistance;
    public float remainingMovement;
    public int numOfActions;
    
    //Misc vars
    public static int numberOfActors = 0;
    public StateMachine SM;
    private AfterActionReport report;
    private Animator playerAnim;
    protected RangeHighlight rangeMarker;
    private bool incapacitated;
    private bool dead;
    protected int deathTimer;
    private Transform mainCamera;
    //Audio clips

    [System.Serializable]
    public class AudioClips
    {
        public AudioClip Move;

        public AudioClip Attack;
        public AudioClip Damage;
        public AudioClip Death;
    }

    public AudioClips soundFx;
    protected AudioSource audio;

    #endregion

    /******************
     *  Events
     ******************/

    #region events
    public void Start()
    {
        Init();
    }

    private void Awake()
    {
        TurnBehaviour.OnUnitSpawn += this.OnUnitSpawn;
        TurnBehaviour.OnTurnStart += this.ActorTurnStart;
        TurnBehaviour.OnUnitMoved += this.ActorMoved;
    }
    
    public virtual void Update()
    {
        if (playerAgent != null)
            anim.SetFloat("Speed", playerAgent.velocity.magnitude);
    }

    public virtual void OnDestroy()
    {
        TurnBehaviour.OnUnitSpawn -= this.OnUnitSpawn;
        TurnBehaviour.OnTurnStart -= this.ActorTurnStart;
        TurnBehaviour.OnUnitMoved -= this.ActorMoved;
    }

    public virtual void ActorTurnStart()
    {
        remainingMovement = moveDistance;

        //Dont want actor making moves if incapacitaded/dead
        if(incapacitated == true || dead == true)
            setNumOfActions(0);
        else
            setNumOfActions(2);

        if(incapacitated == true && deathTimer < 6)
        {
            deathTimer++;
            Debug.Log("Death Timer : " + deathTimer);
        }
        if(deathTimer == 6)
        {
            gameObject.SetActive(false);
        }
        if(counterAttack > 0)
        {
            counterAttack--;
        }
        //if(report != null)
        //{
        //    report.BattleOver();    //checks for win/lose conditions and loads hub
        //}

    }

    public virtual void ActorMoved()
    {
        if (rangeMarker != null)
            rangeMarker.Marker_Off();
    }

    #endregion

    protected void Init()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        if (GameObject.Find("SceneManager") != null)
        {
            report = GameObject.Find("SceneManager").GetComponent<AfterActionReport>();
        }
        
        incapacitated = false; //determines whether actor is knocked out
        dead = false;          //perma death

        //NOTE: AR problems can happen here.
        coords.x = transform.position.x;
        coords.y = transform.position.y;
        coords.z = transform.position.z;

        health_current = health_max;
        remainingMovement = moveDistance;
        numOfActions = 2;


        anim = GetComponentInChildren<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();
        GameObject rangeMarkerObj = GameObject.Find("RangeMarker");
        if (rangeMarkerObj != null)
            rangeMarker = rangeMarkerObj.GetComponent<RangeHighlight>();

        if (GameObject.FindWithTag("GameController") == null)
        {
            Debug.LogError("Missing Game Controller, add in scene hierarchy");
            return;
        }
        SM = GameObject.FindWithTag("GameController").GetComponent<StateMachine>();

        if (map == null)
        {
            return;
        }
        map = GameObject.Find("Map").GetComponent<TileMap>();

        if (map.IsValidCoord(coords) == true)
        {
            Debug.Log("Coords: " + coords);
            map.GetTileAt(coords).setOccupiedTrue(gameObject);
            Debug.Log("Occupied = " + map.GetTileAt(coords).isOccupied());
        }


        //map.getMapArray()[tileX, tileZ].occupied = true;
        //Debug.Log(map.getMapArray()[tileX, tileZ].occupied);

    }

    //Player Spawn Event - Put any actions you want done upon player spawn in here
    public void OnUnitSpawn()
    {
        
        
       

    }
    

    /// <summary>
    /// Controls the physical and animation of moving the actor.  Does not generate path.
    /// </summary>
    /// <param name="origin">The object you want to move</param>
    /// <param name="targetPos">The World position of the move</param>
    /// <param name="speed">The speed of the move</param>
    /// <returns>False if the move is not done, true if the move is done.</returns>
    public bool MoveController(Transform origin, Vector3 targetPos, float speed)
    {
        float scaleDist = 1f;
        float dist = Vector3.Distance(origin.position, targetPos);
        if (dist < 0.26f) //old dist .01
        {
            origin.position = targetPos;
            scaleDist = 0f;
            if (anim != null && playerAgent == null)
            {
                anim.SetFloat("Speed", scaleDist);
                anim.SetBool("Moving", true);
                anim.SetFloat("Velocity Z", scaleDist);
            }

            return true;
        }


        float step = speed * Time.deltaTime * scaleDist;

        if (playerAgent != null)
        {
            //Debug.Log("dist = " + dist + " target position = " + targetPos);
            playerAgent.destination = targetPos;
            
        }
        else
        {

            if (anim != null)
            {
                anim.SetFloat("Speed", scaleDist);
                anim.SetBool("Moving", true);
                anim.SetFloat("Velocity Z", scaleDist);
            }

            origin.position = Vector3.MoveTowards(origin.position, targetPos, step);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPos, speed, 0f);
            newDir = new Vector3(newDir.x, origin.position.y, newDir.z);

            newDir = new Vector3(targetPos.x, origin.position.y, targetPos.z);
            origin.transform.LookAt(newDir);
        }


        return false;
    }

    /// <summary>
    /// Plays an audioClip attached to actor.
    /// </summary>
    /// <param name="input">string of the soundFx var</param>
    /// <returns>True = sucess, False = failed to play</returns>
    public bool PlaySound(string input)
    {
        if (soundFx == null
            || soundFx.Move == null
            || soundFx.Attack == null
            || soundFx.Damage == null
            || soundFx.Death == null
            || audio == null)
            return false;
        //justin audio garbage - ignore until fixed
        //   int n;
        //   n = UnityEngine.Random.Range(1, 6);
        //   int k;
        //   k = UnityEngine.Random.Range(1,4);
        //   bool t;
        //   t = getCurrentTurn();




        switch (input.ToLower())
        {

            case "move":
                audio.clip = soundFx.Move;
                break;
            case "attack":
                audio.clip = soundFx.Attack;

               //justin audio stuff - ignore
               //audio.clip = soundFx.PlayerAttackSounds[n-1];
              
                break;
            case "damage":
                audio.clip = soundFx.Damage;
                break;
            case "death":
                audio.clip = soundFx.Death;
                break;
            default:
                return false;
        }

        audio.Play();
        return true;
    }

    void clickToMove()
    {
        if (Input.GetMouseButtonDown(0) &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
    }

    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Interactable")
            {
                Debug.Log("Interactable object");
            }
            else
            {
                //move our player to the point
                playerAgent.destination = interactionInfo.point;
                
            }
        }
    }

    protected void rotateAtObj(GameObject target)
    {
        Vector3 newDir = Vector3.RotateTowards(gameObject.transform.forward, target.transform.position, 1f, 0f);
        newDir = new Vector3(newDir.x, gameObject.transform.position.y, newDir.z);

        newDir = new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z);
        gameObject.transform.LookAt(newDir);
    }

    #region damage/heal functions

    /// <summary>
    /// The method to damage an Actor
    /// </summary>
    /// <param name="damage">Damage the Actor will take as a float</param>
    public virtual void TakeDamage(float damage, GameObject attacker)
    {
        float dist = Vector3.Distance(getCoords(), attacker.GetComponent<Actor>().getCoords());
        if (counterAttack > 0 )
        {
            Debug.Log("Attempting Counter Attack. " + dist);
            CounterAttack(attacker);
            return;
        }
        if (damage < 0)
        {
            Debug.Log("Damage is negative. Value = " + damage);
            return;
        }

        damageNumber(damage, new Color(255, 0, 0, 1));

        health_current -= damage;
        Debug.Log(name + " has taken " + damage + " Current Health = " + health_current);
        if (health_current <= 0)
        {
            health_current = 0;
            OnDeath();
            return;
        }
        
        anim.SetTrigger("Hit");
        //justin set damage string array here
        PlaySound("damage");
        //Debug.Log(name + " has taken " + damage + " Current Health = " + health_current);
        if (gameObject.GetComponentInChildren<HealthBar>() != null)
        {
            gameObject.GetComponentInChildren<HealthBar>().updateHealth(GetHealthPercent());
        }
    }

    public bool UseMana(float cost)
    {
        if (cost == 0)
            return true;
        if(this.mana_current >= cost)
        {
            this.mana_current -= cost;
            return true;
        }

        return false;
    }

    public void CounterAttack(GameObject target)
    {
        float damage = 5f + ((float)this.strength * 0.5f);
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", "Counter Attack", gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MeleeAttack");
            //justin set attack string array choice hereS
            PlaySound("attack");
        }
        target.GetComponent<Actor>().TakeDamage(damage, this.gameObject);
    }

    /// <summary>
    /// Spawns damage number over the actors head, first parameter is damage
    /// and second is the color of the text
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="color"></param>
    public void damageNumber(float damage, Color color)
    {
        TextMesh text = Resources.Load<GameObject>("Damage").GetComponent<TextMesh>();
        text.text = damage.ToString();
        TextMesh instance = Instantiate(text);
        instance.transform.SetParent(transform);
        instance.transform.LookAt(GameObject.FindWithTag("MainCamera").transform);
        instance.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-.2f, .2f), 2, 0);
        instance.color = color;
        instance.gameObject.GetComponent<Rigidbody>().velocity = transform.up;
        Destroy(instance.gameObject, 1);
    }

    public virtual void HealHealth(float heal)
    {
        health_current += heal;
        if (health_current > health_max)
        {
            health_current = health_max;
        }
        damageNumber(heal, new Color(0, 255, 0, 1));
    }

    public virtual void OnDeath()
    {
        incapacitated = true;
        anim.SetTrigger("Death");
        PlaySound("death");
    }
    #endregion
    
    /******************
    *  Set/Gets
    ******************/

    #region SetGets

    public bool canAct()
    {
        if(numOfActions > 0)
        {
            return true;
        }
        return false;
    }

    public bool useAction()
    {
        if(numOfActions <= 0)
        {
            numOfActions = 0;
            return false;
        }

        if (numOfActions > 0)
        {
            numOfActions--;
        }
        return true;
    }

    public int actionNumber()
    {
        return numOfActions;
    }

    public List<Nodes> getCurrentPath()
    {
        return currentPath;
    }

    public void setCurrentPath(List<Nodes> path)
    {
        currentPath = path;
    }

    public void setPathNull()
    {
        currentPath = null;
    }

    public void setCoords(Vector3 coordinates)
    {
        coords = coordinates;
    }

    public Vector3 getCoords()
    {
        return coords;
    }

    public void setSpeed(int num)
    {
        speed = num;
    }

    public bool isIncapacitated()
    {
        return incapacitated;
    }

    public bool isDead()
    {
        return dead;
    }
    
    public float getSpeed()
    {
        return speed;
    }

    public void setNumOfActions(int moves)
    {
        numOfActions = moves;
    }

    public int getMoves()
    {
        return numOfActions;
    }

    public void setMoveDistance(int distance)
    {
        moveDistance = distance;
    }

    public void setRemainingMovement(float movement)
    {
        remainingMovement = movement;
    }

    public float getRemainingMovement()
    {
        return remainingMovement;
    }

    public int getMoveDistance()
    {
        return moveDistance;
    }

    public void setMovement(int movesLeft)
    {
        remainingMovement = movesLeft;
    }

    public void setCounterAttack(int number_of_counters)
    {
        counterAttack = number_of_counters;
    }

    public float GetHealthPercent()
    {
        float hpPercent = health_current / health_max;
        if (hpPercent <= 0f)
            return 0f;
        else if (hpPercent >= 1f)
            return 1f;

        return hpPercent;
    }

    public float GetHealthCurrent()
    {
        return health_current;
    }

    public void setHealthCurrent(float health)
    {
        health_current = health;
    }

    public float GetHeathMax()
    {
        return health_max;
    }

    public float getManaCurrent()
    {
        return mana_current;
    }

    public void setManaCurrent(float mana)
    {
        mana_current = mana;
    }

    public void setArmorClass(float aClass)
    {
        armor_class = aClass;
    }

    public float getArmorClass()
    {
        return armor_class;
    }
    
    public int getNumofActors()
    {
        return numberOfActors;
    }

    //justin added v
    public bool getCurrentTurn()
    {
        return TurnBehaviour.IsPlayerTurn();
    }
    //justin added ^
    
    //=======Stat Set/Gets===========//

    public void setMaxHealth(int health)
    {
        health_max = health;
    }

    public void setMaxMana(float mana)
    {
        mana_max = mana;
    }

    public float getMaxMana()
    {
        return mana_max;
    }

    public void setStrength(int str)
    {
        strength = str;
    }

    public int getStrength()
    {
        return strength;
    }

    public void setDexterity(int dex)
    {
        dexterity = dex;
    }

    public int getDexterity()
    {
        return dexterity;
    }

    public void setConstitution(int con)
    {
        constitution = con;
    }

    public int getConstitution()
    {
        return constitution;
    }

    public void setIntelligence(int intel)
    {
        intelligence = intel;
    }
    
    public int getIntelligence()
    {
        return intelligence; 
    }

    public void setWisdom(int wis)
    {
        wisdom = wis;
    }

    public int getWisdom()
    {
        return wisdom;
    }

    public void setCharisma(int cha)
    {
        charisma = cha;
    }

    public int getCharisma()
    {
        return charisma;
    }

    public void setExperience(int xp)
    {
        experience = xp;
    }

    public int getExperience()
    {
        return experience;
    }
    #endregion
}
