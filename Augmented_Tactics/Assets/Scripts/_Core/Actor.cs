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

    //Added by arthur ==========================
    private TileMap map;
    private StateMachine SM;
    Vector3[] position;
    LineRenderer path;
    public int tileX;
    public int tileZ;
    public int index;
    public float speed;
    private int moveDistance;
    private float remainingMovement;
    static public int numberOfActors = 0;
    private int numOfMoves;
    
    //===========================================
    #endregion


    /******************
     *  Events
     ******************/
    #region Events
    // Use this for initialization
    public virtual void Start ()
    {
                
        initialize();
       
    }

   
    public void initialize()
    {
        numOfMoves = 1;

        //number of tiles
        moveDistance = 4;

        anim = GetComponentInChildren<Animator>();

        if (GameObject.Find("Path").GetComponent<LineRenderer>() == null)
        {
            Debug.LogError("Missing path, make sure to include in level hierarchy");
            return;
        }
        path = GameObject.Find("Path").GetComponent<LineRenderer>();

        if (GameObject.FindWithTag("GameController") == null)
        {
            Debug.LogError("Missing GameController, make sure to include in level hierarchy");
            return;
        }
        SM = GameObject.FindWithTag("GameController").GetComponent<StateMachine>();

        if(GameObject.Find("Map").GetComponent<TileMap>() == null)
        {
            Debug.LogError("Missing map, make sure to include in level hierarchy");
        }
        map = GameObject.Find("Map").GetComponent<TileMap>();

    }


    // Update is called once per frame
    public virtual void Update () {


    }

    private void OnMouseExit()
    {     
        path.positionCount = 0;
    }
    private void OnMouseUp()
    {
        if (currentPath != null)
        {
            position = new Vector3[currentPath.Count];
            path.SetPositions(position);
        }
        
        //sets gameobject in tilemap to object clicked on
        map.setSelectedUnit(gameObject);  
      
    }

    #endregion


    /// <summary>
    /// The method to damage an Actor
    /// </summary>
    /// <param name="damage">Damage the Actor will take as a float</param>
    /// 

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

    bool MoveController(Transform origin, Vector3 targetPos, float speed)
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

    

    public void NextTurn()
    {
        

        map.Players[index].coordX = tileX;
        map.Players[index].coordZ = tileZ;
        map.Players[index].coords = new Vector3(tileX, 0, tileZ);
       
        for (int index = 0; index < numberOfActors; index++)
        {
            //GO.Players[index] = new TileMap.Location();
            //Debug.Log("test" + index + "tileX " + tileX);
            
            map.Players[index].coordX = tileX;
            map.Players[index].coordZ = tileZ;
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

    public void setMoves(int moves)
    {
        numOfMoves = moves;
    }

    public int getMoves()
    {
        return numOfMoves;
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

    #endregion
}
