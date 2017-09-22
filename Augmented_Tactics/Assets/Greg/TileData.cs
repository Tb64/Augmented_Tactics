using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {
    public GameObject tilePrefab;
    public Vector3 position;
    public float tileScale;
    void Start(){
        //tile.transform.localScale = new Vector3(tileScale, tileScale, tileScale);
    }
}
