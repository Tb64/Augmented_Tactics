using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public int tileClass;
    public TileMap map;
    public TileType tileTypes;
    private Color32 originalColor;
    Actor unit;
    public bool occupied;
    StateMachine controller;
    Actor player;
    Actor enemy;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Actor>();
        unit = GameObject.FindWithTag("Map").GetComponent<TileMap>().selectedUnit.GetComponent<Actor>();
        controller = GameObject.Find("GameController").GetComponent<StateMachine>();
        //sets clickable tile to false as its initialized
        occupied = false;
    }
   
    private void Update()
    {
     
    }

    public void OnMouseUp()
    {
        //single click
        Debug.Log("Click");
        //Generates a path to clicked tile

        bool firstClick = true;

        if (controller.getFirstTurn() == true && firstClick == true)
        {//Only runs on the first click of the scene
            map.getMapArray()[enemy.GetComponent<Actor>().tileX,
                enemy.GetComponent<Actor>().tileZ].setOccupiedTrue();
            map.getMapArray()[player.GetComponent<Actor>().tileX,
                player.GetComponent<Actor>().tileZ].setOccupiedTrue();
            firstClick = false;
        }

        if (map.getEndOfMove() == true)
        {
            map.GeneratePathTo(tileX, tileZ);
        }
       
        //map.moveActor(Unit, new Vector3(5, 0, 5));

        
        //Unit.NextTurn();
    }

    public void OnMouseEnter()
    {
        //highlights block that mouse hovers over
        originalColor = gameObject.GetComponent<MeshRenderer>().material.color;
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(150,248,43,255));

    
    }

    public void OnMouseExit()
    {
        //resets block back to original color after mouse pointer exits
        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void setOccupiedTrue()
    {
        occupied = true;
    }
    public void setOccupiedFalse()
    {
        occupied = false;
    }
}

