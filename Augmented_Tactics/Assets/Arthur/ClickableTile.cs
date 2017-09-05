using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public int tileClass;
    public TileMap map;

    public TileType tileTypes;


    public void OnMouseUp()
    {
        Debug.Log("click");
        map.GeneratePathTo(tileX, tileY);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

