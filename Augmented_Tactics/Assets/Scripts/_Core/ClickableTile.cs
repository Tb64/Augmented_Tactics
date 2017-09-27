using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;

    //holds player x,y,z coords
    Vector3 playerCoords;
    
    //true if an actor is standing on the tile
    private bool occupied;


    public int tileClass;
    public TileMap map;

    //used to store original color of tile on mouseOver
    private Color32 originalColor;

    //delay between 2 clicks
    float delay = .3f;
    


    float deltaTime;

    public TileType tileTypes;

    
   
    private void Start()
    {
        //sets each tile to not occupied by default
        occupied = false;
        deltaTime = 0;
        
    }
   
    private void Update()
    {
        deltaTime += Time.deltaTime;
    }

    #region mouseEvents
    public void OnMouseUp()
    {
        //single click

        //Generates a path to clicked tile
        map.GeneratePathTo(tileX, tileZ);
        Debug.Log("click");
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
    #endregion
    #region setGets

    //returns current gameobject
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    //sets the tile to occupied
    public void setOccupied()
    {
        occupied = true;
    }
    //returns whether tile is occupied or not
    public bool isOccupied()
    {
        return occupied;
    }
    #endregion
}

