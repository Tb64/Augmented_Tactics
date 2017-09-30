using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public int tileClass;
    public TileMap map;
    private Color32 originalColor;
    float deltaTime;

    public TileType tileTypes;

    public bool occupied;
   
    private void Start()
    {
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
        map.GeneratePathTo(tileX, tileZ);

        //GameObject Unit;
        //Unit = GameObject.FindWithTag("Map").GetComponent<TileMap>().selectedUnit;
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

   

}

