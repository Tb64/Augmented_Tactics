using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public int tileClass;
    public TileMap map;

    public TileType tileTypes;

    public bool occupied;
   


    private void Start()
    {
        occupied = false;
        tileX = (int)transform.position.x;
        tileZ = (int)transform.position.z;
    }

    private void Update()
    {
        
    }

    public void OnMouseUp()
    {
        Debug.Log("click");
        map.GeneratePathTo(tileX, tileZ);
    }



    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

