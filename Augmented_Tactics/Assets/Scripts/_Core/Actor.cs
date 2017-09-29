using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;

public class Actor : MonoBehaviour {

    /******************
     *  Variables
     ******************/

    #region Variables

    protected Animator anim;

    protected float health_current;
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
    private float remainingMovement;
    private List<Node> currentPath = null;
    static public int numberOfActors = 0;
    public int numOfMoves;


    //===========================================
    #endregion


    /******************
     *  Events
     ******************/

    #region events
    public virtual void Start ()
    {
        Initialize();
    }

    
    public virtual void Update()
    {
        //drawDebugLines();
        //moveUnit();
    }

    private void Awake()
    {
        map = GameObject.Find("Map").GetComponent<TileMap>();
        if (map.getMapArray() != null)
        {
            map.getMapArray()[tileX, tileZ].occupied = true;
            Debug.Log(map.getMapArray()[tileX, tileZ].occupied);
        }

    }
    #endregion

    #region mouseEvents

    private void OnMouseUp()
    {
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        GO.selectedUnit = gameObject;
    }
    
    #endregion


    private void Initialize()
    {
        //number of moves each actor can make per turn
        numOfMoves = 1;
        anim = GetComponentInChildren<Animator>();
        
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
        //map = GameObject.Find("Map").GetComponent<TileMap>();

        //map.getMapArray()[tileX, tileZ].occupied = true;
        //Debug.Log(map.getMapArray()[tileX, tileZ].occupied);
    }
        
    public bool MoveController(Transform origin, Vector3 targetPos, float speed)
    {
        float scaleDist = 1f;

        if (Vector3.Distance(origin.position, targetPos) < 0.01f)
        {
            origin.position = targetPos;
            scaleDist = 0f;
            if (anim != null)
                anim.SetFloat("Speed", scaleDist);
            return true;
        }

        if (anim != null)
            anim.SetFloat("Speed", scaleDist);

        float step = speed * Time.deltaTime * scaleDist;
        origin.position = Vector3.MoveTowards(origin.position, targetPos, step);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPos, speed, 0f);
        newDir = new Vector3(newDir.x, origin.position.y, newDir.z);


        newDir = new Vector3(targetPos.x, origin.position.y, targetPos.z);
        origin.transform.LookAt(newDir);


        return false;
    }
        
    /// <summary>
    /// The method to damage an Actor
    /// </summary>
    /// <param name="damage">Damage the Actor will take as a float</param>
    /// 

    #region damage/heal functions
    public virtual void TakeDamage(float damage)
    {
        health_current -= damage;
        if (health_current <= 0)
        {
            health_current = 0;
            OnDeath();
        }
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
        
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        
        if (GO == null)
        {
            return;
        }

        GO.Players[index].coordX = tileX;
        GO.Players[index].coordZ = tileZ;
        GO.Players[index].coords = new Vector3(tileX, 0, tileZ);
       
        for (int index = 0; index < numberOfActors; index++)
        {
            //GO.Players[index] = new TileMap.Location();
                        
            GO.Players[index].coordX = tileX;
            GO.Players[index].coordZ = tileZ;
        }

        //Reset available movement points.
        
        if (numOfMoves != 0)
        {
            numOfMoves--;
            remainingMovement = moveDistance;
        }
        
    }



    /******************
    *  Set/Gets
    ******************/

    #region SetGets
  
    
    public List<Node> getCurrentPath()
    {
        return currentPath;
    }

    public void setPathNull()
    {
        currentPath = null;
    }

    public void setCoords(Vector3 coordinates)
    {
        coords = coordinates;
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

    public float GetHeathMax()
    {
        return health_max;
    }

    #endregion
}
