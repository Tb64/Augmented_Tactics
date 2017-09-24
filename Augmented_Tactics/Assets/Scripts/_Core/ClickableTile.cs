using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public int tileClass;
    public TileMap map;
    private Color32 originalColor;
    float delay = .2f;
    float deltaTime;

    public TileType tileTypes;

    public bool occupied;
   
    private void Start()
    {
        occupied = false;
        deltaTime = 0;
        
    }
   
    private void Update()
    {
        deltaTime += Time.deltaTime;
    }

    public void OnMouseUp()
    {
        //single click

        //Generates a path to clicked tile
        map.GeneratePathTo(tileX, tileZ);

        Actor Unit;    
        Unit = GameObject.FindWithTag("Map").GetComponent<TileMap>().selectedUnit.GetComponent<Actor>();
        
        //check for double click
        if (deltaTime < delay)
        {
            //double click
            Unit.NextTurn();
            Debug.Log("Double Click!");
        }
        deltaTime = 0; // resets delta time for double click detection
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

    public void OnMouseOver()
    {
       
        if(GameObject.FindWithTag("Player").GetComponent<Actor>().getMoves() == 0)
        {
            return;
        }
        map.GeneratePathTo(tileX, tileZ);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

