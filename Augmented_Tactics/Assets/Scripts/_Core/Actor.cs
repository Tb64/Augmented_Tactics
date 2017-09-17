﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;

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
    protected bool enemyTurn;
    public TileMap map;
    public StateMachine SM;
    public float speed;
    public int moveDistance;
    float step;
    float remainingMovement;
    public List<Node> currentPath = null;
    static public int numberOfActors = 0;
    float delay = .3f;
    float deltaTime;
    //===========================================

    // Use this for initialization
    public virtual void Start ()
    {
        deltaTime = 0;
        if(GameObject.FindWithTag("GameController") == null)
        {
            Debug.Log("Missing state machine");
            return;
        }
        SM = GameObject.FindWithTag("GameController").GetComponent<StateMachine>();

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }
       
	}

    // Update is called once per frame
    public virtual void Update () {

        deltaTime += Time.deltaTime;

        //drawDebugLines();

        //moveUnit();

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
            //Debug.Log("X " + tileX + "Y " + tileZ + "name" + gameObject.name);
        }
        //move unit to next tile
        //Debug.Log("X " + tileX + "Y " + tileZ + "name" + gameObject.name);
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
        
        if (enemyTurn == true)
        {
            return;
        }
       

        // Get cost from current tile to next tile
        remainingMovement -= map.costToEnterTile(currentPath[0].x, currentPath[0].z, currentPath[1].x, currentPath[1].z);
        //Debug.Log("X0 " + currentPath[0].x + "Z0 " + currentPath[0].z + "X1 " +
        //  currentPath[1].x + "Z1 " + currentPath[1].z+ "Name " + gameObject.name );
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
        Debug.Log("next turn" + numberOfActors);

        //foreach (Actor player in GameObject.FindWithTag("Map").GetComponent<TileMap>().Players)
        //{
        //    player.tileX = tileX;
        //    player.tileZ = tileZ;

        //}
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        if(GO == null)
        {
            return;
        }

        GO.Players[index].coordX = tileX;
        GO.Players[index].coordZ = tileZ;

        for (int index = 0; index < numberOfActors; index++)
        {
            //GO.Players[index] = new TileMap.Location();
            //Debug.Log("test" + index + "tileX " + tileX);
            
            GO.Players[index].coordX = tileX;
            GO.Players[index].coordZ = tileZ;
        }

        
        //Reset available movement points.
        remainingMovement = moveDistance;

    }

    

    private void OnMouseOver()
    {
        //hightlight player when mouse is hovering over
    }
    //========================================================

    private void OnMouseUp()
    {
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        //Button button = GameObject.FindWithTag("Button").GetComponent<Button>();
       
        //double click detection
        if (deltaTime < delay)
        {
            GO.selectedUnit = gameObject;
            remainingMovement = moveDistance;
            // button.onClick.AddListener

        }
        deltaTime = 0;
    }

}
