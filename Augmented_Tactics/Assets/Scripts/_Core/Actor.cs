using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Actor : MonoBehaviour {

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
    public float speed;
    public int moveDistance;
    float step;
    float remainingMovement;
    public List<Node> currentPath = null;
    static public int numberOfActors = 0;
    //===========================================

    // Use this for initialization
    void Start ()
    {

        numberOfActors++;
        

        if (map == null)
        {
            map = GameObject.Find("map").GetComponent<TileMap>();
        }
        		
	}
	
	// Update is called once per frame
	void Update () {

        drawDebugLines();
        moveUnit();

    }

    /// <summary>
    /// The method to damage an Actor
    /// </summary>
    /// <param name="damage">Damage the Actor will take as a float</param>
    /// 

    public void TakeDamage(float damage)
    {

    }

    public void HealHealth(float heal)
    {

    }


    //Added by Arthur===========================================
    //Draws pathing lines
    public void drawDebugLines()
    {
        if (currentPath != null)
        {
            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].z) +
                    new Vector3(0, 1f, 0);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].z) +
                    new Vector3(0, 1f, 0);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }
    }

    public void moveUnit()
    {
        if (Vector3.Distance(transform.position, map.TileCoordToWorldCoord(tileX, tileZ)) < 0.1f)
        {
            AdvancePathing();
        }
        //move unit to next tile
        MoveController(transform, map.TileCoordToWorldCoord(tileX, tileZ), speed);
        //transform.position = Vector3.MoveTowards(transform.position, map.TileCoordToWorldCoord(tileX, tileZ), speed * Time.deltaTime);
    }

    bool MoveController(Transform origin, Vector3 targetPos, float speed)
    {
        float step = speed * Time.deltaTime;
        origin.position = Vector3.MoveTowards(origin.position, targetPos, step);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPos, speed, 0f);
        newDir = new Vector3(newDir.x, origin.position.y, newDir.z);


        newDir = new Vector3(targetPos.x, origin.position.y, targetPos.z);
        origin.transform.LookAt(newDir);
        if (Vector3.Distance(origin.position, targetPos) < 0.001f)
            return true;
        return false;
    }
    void AdvancePathing()
    {
        if (currentPath == null)
            return;

        if (remainingMovement <= 0)
            return;

        // Get cost from current tile to next tile
        remainingMovement -= map.costToEnterTile(currentPath[0].x, currentPath[0].z, currentPath[1].x, currentPath[1].z);

        // Move us to the next tile in the sequence
        tileX = currentPath[1].x;
        tileZ = currentPath[1].z;

        // Remove the old "current" tile from the pathfinding list
        currentPath.RemoveAt(0);

        if (currentPath.Count == 1)
        {
            //standing on same tile clicked on
            currentPath = null;
        }
    }

    public void NextTurn()
    {
        Debug.Log("next turn");

        //foreach (Actor player in GameObject.FindWithTag("Map").GetComponent<TileMap>().Players)
        //{
        //    player.tileX = tileX;
        //    player.tileZ = tileZ;

        //}

        for (int index = 0; index < numberOfActors; index++)
        {
            GameObject.FindWithTag("Map").GetComponent<TileMap>().Players[index].coordX = tileX;
        }

        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        for (int index = 0; index < numberOfActors; index++)
        {
            Debug.Log("index" + index + "x"+ GO.Players[index].coordX + "z" +GO.Players[index].coordZ);
        }
        //Reset available movement points.
        remainingMovement = moveDistance;

    }


    private void OnMouseOver()
    {
        //hightlight player when mouse is hovering over
    }
    //========================================================



}
