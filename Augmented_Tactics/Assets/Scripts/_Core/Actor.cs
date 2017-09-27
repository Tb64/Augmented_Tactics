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
    public int tileX;
    public int tileZ;
    public int index;
    public TileMap map;
    public StateMachine SM;
    public float speed;
    public int moveDistance;
    float step;
    public float remainingMovement;
    public List<Node> currentPath = null;
    static public int numberOfActors = 0;
    float delay = .3f;
    float deltaTime;
    public int numOfMoves;
    Vector3[] position;
    LineRenderer path;
    //===========================================
    #endregion


    /******************
     *  Events
     ******************/
    #region Events
    // Use this for initialization
    public virtual void Start ()
    {
        numOfMoves = 1;
        anim = GetComponentInChildren<Animator>();
        

        if (GameObject.Find("Path").GetComponent<LineRenderer>() != null)
        {
            path = GameObject.Find("Path").GetComponent<LineRenderer>();
        }

        deltaTime = 0;
        if(GameObject.FindWithTag("GameController") == null)
        {
            Debug.Log("Missing state machine");
            return;
        }
        SM = GameObject.FindWithTag("GameController").GetComponent<StateMachine>();

        map = GameObject.Find("Map").GetComponent<TileMap>();
       
    }

   
    // Update is called once per frame
    public virtual void Update () {

        deltaTime += Time.deltaTime;

        //drawDebugLines();

        //moveUnit();

    }

    public void OnMouseOver()
    {


    }

    private void OnMouseEnter()
    {
        

    }

    private void OnMouseExit()
    {
                
        path.positionCount = 0;
        
        
    }
    private void OnMouseUp()
    {

        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        if (currentPath != null)
        {
            position = new Vector3[currentPath.Count];
            path.SetPositions(position);
        }
        

        //double click detection
        if (deltaTime < delay)
        {
            //sets gameobject in tilemap to object clicked on
            GO.selectedUnit = gameObject;  
        }
        deltaTime = 0;
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

    //Draws pathing lines
    public void drawDebugLines()
    {

        if (currentPath != null)
        {
            int currNode = 0;
            Vector3[] position = new Vector3[currentPath.Count];
            Vector3 end = new Vector3();
            while (currNode < currentPath.Count - 1 && currentPath.Count < moveDistance + 2)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].z) +
                    new Vector3(0, 1f, 0);
                end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].z) +
                    new Vector3(0, 1f, 0);

                Debug.DrawLine(start, end, Color.red);
             
                path.positionCount = currentPath.Count - 1;
                position[currNode] = start;
                path.SetPositions(position);
                currNode++;
                
                if (currNode  == currentPath.Count - 1)
                {
                    position[currNode] = end;
                }
      
            }
        }
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
            //Debug.Log("test" + index + "tileX " + tileX);
            
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

    public void setMoves(int moves)
    {
        numOfMoves = moves;
    }

    public int getMoves()
    {
        return numOfMoves;
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
