using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ClickableTile[] tiles = GetComponentsInChildren<ClickableTile>();

        for(int index = 0; index < tiles.Length; index++)
        {
            tiles[index].tileX = (int)tiles[index].transform.position.x;
            tiles[index].tileZ = (int)tiles[index].transform.position.z;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
