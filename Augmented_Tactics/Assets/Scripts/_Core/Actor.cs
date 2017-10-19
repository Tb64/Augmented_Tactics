using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;

public class Actor : TurnBehavoir
{

    /******************
     *  Variables
     ******************/

    #region Variables

    protected Animator anim;

    public float health_current;    //temporary for debugging purposes(should be protected)
    protected float health_max; 
    protected float mana_current;
    protected float mana_max;
    protected float move_speed;
    protected float armor_class;

    //stats
    protected int strength;         //measuring physical power
    protected int dexterity;        //measuring agility
    protected int constitution;     //measuring endurance
    protected int intelligence;     //measuring reasoning and memory (Magic Damage)
    protected int wisdom;           //measuring perception and insight (Resistance/Healing)
    protected int charisma;         //measuring force of personality (Buffs and Debuffs)

    public Ability[] abilitySet;

    //Added by arthur ==========================
    public int tileX;
    public int tileZ;
    public int index;
    private Vector3 coords;
    public TileMap map;
    public StateMachine SM;
    public float speed;
    public int moveDistance;
    public float remainingMovement;
    private List<Node> currentPath = null;
    static public int numberOfActors = 0;
    public int numOfMoves;
    private bool canMove;
    private bool moveClicked;
    NavMeshAgent playerAgent;
    private Animator playerAnim;

    //===========================================
    #endregion

    /******************
     *  Events
     ******************/

    #region events
    public virtual void Start()
    {
        Init();
       
    }

    private void Awake()
    {
        
    }

    public virtual void Update()
    {

        if (playerAgent == null) //bandaid fix, needs to be removed
        {
            return;
        }
        //clickToMove();
        anim.SetFloat("Speed", playerAgent.velocity.magnitude);
       
    }

    #endregion
        
    #region mouseEvents


    public void OnMouseUp()
    {
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        Debug.Log("click test");
        GO.selectedUnit = gameObject;
    }

    #endregion
        
    private void Init()
    {
        //number of moves each actor can make per turn
        numOfMoves = 2;
        anim = GetComponentInChildren<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();

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

        //map.getMapArray()[tileX, tileZ].occupied = true;
        //Debug.Log(map.getMapArray()[tileX, tileZ].occupied);
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
                anim.SetFloat("Speed", scaleDist);
            return true;
        }

       
        float step = speed * Time.deltaTime * scaleDist;

        if (playerAgent != null)
        {
            Debug.Log("dist = " + dist + " target position = " + targetPos);
            playerAgent.destination = targetPos;
        }
        else
        {

            if (anim != null)
                anim.SetFloat("Speed", scaleDist);

            origin.position = Vector3.MoveTowards(origin.position, targetPos, step);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPos, speed, 0f);
            newDir = new Vector3(newDir.x, origin.position.y, newDir.z);

            newDir = new Vector3(targetPos.x, origin.position.y, targetPos.z);
            origin.transform.LookAt(newDir);
        }


        return false;
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

    #region damage/heal functions

    /// <summary>
    /// The method to damage an Actor
    /// </summary>
    /// <param name="damage">Damage the Actor will take as a float</param>
    public virtual void TakeDamage(float damage)
    {
        health_current -= damage;
        if (health_current <= 0)
        {
            health_current = 0;
            OnDeath();
        }

        Debug.Log(name + " has taken " + damage + " Current Health = " + health_current);
    }

    public virtual void HealHealth(float heal)
    {
        health_current += heal;
        if (health_current > health_max)
        {
            health_current = health_max;
        }
    }

    public virtual void OnDeath()
    {

    }
    #endregion
    
    public void NextTurn()
    {
        canMove = true;
        moveClicked = true;
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        
        if (GO == null)
        {
            return ;
        }

        
      
        //Reset available movement points.
        
        if (numOfMoves != 0 && currentPath != null )
        {
            numOfMoves--;
            remainingMovement = moveDistance;
        }
        
    }

    /******************
    *  Set/Gets
    ******************/

    #region SetGets
  
    public Vector3 getMapPosition()
    {
        return new Vector3((float)tileX, 0f, (float)tileZ);
    }
    
    public List<Node> getCurrentPath()
    {
        return currentPath;
    }

    public void setCurrentPath(List<Node> path)
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

    
    public float getSpeed()
    {
        return speed;
    }

    public void setMoves(int moves)
    {
        numOfMoves = moves;
    }

    public int getMoves()
    {
        return numOfMoves;
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

    public void setHealthCurrent(int health)
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

    public void setCanMove(bool trueFalse)
    {
        canMove = trueFalse;
    }

    public void setArmorClass(float aClass)
    {
        armor_class = aClass;
    }

    public float getArmorClass()
    {
        return armor_class;
    }
    public bool getCanMove()
    {
        return canMove;
    }

    public int getNumofActors()
    {
        return numberOfActors;
    }


    public bool getMoveClicked()
    {
        return moveClicked;
    }

    public void setMoveClicked(bool tf)
    {
        moveClicked = tf;
    }

    //=======Stat Set/Gets===========//

    public void setMaxHealth(int health)
    {
        health_max = health;
    }

    public float getMaxHealth()
    {
        return health_max;
    }

    public void setHealthCurrent(float health)
    {
        health_current = health;
    }

    public void setMaxMana(int mana)
    {
        mana_max = mana;
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
    #endregion
}
