using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileType{

    public string name;
    public GameObject tileVisualPrefab;
    public bool isWalkable = true;
    public bool isCover = false;

    public float movementCost = 1;
}
