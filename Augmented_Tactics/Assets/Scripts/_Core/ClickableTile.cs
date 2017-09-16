using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public int tileClass;
    public TileMap map;
    private Color originalColor;
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
        map.GeneratePathTo(tileX, tileZ);
        PlayerControlled Unit = GameObject.FindWithTag("Map").GetComponent<TileMap>().selectedUnit.GetComponent<PlayerControlled>();

        //check for double click
        if (deltaTime < delay)
        {
            //double click
            Unit.NextTurn();
            Debug.Log("Double Click!");
            

        }
        deltaTime = 0;
    }

    public void OnMouseEnter()
    {
        originalColor = gameObject.GetComponent<MeshRenderer>().material.color;
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0f,248f,43f,255f));
        
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

