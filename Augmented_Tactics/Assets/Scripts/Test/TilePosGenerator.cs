using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ClickableTile[] tiles = GetComponentsInChildren<ClickableTile>();

        for(int index = 0; index < tiles.Length; index++)
        {
            tiles[index].coords.x = (int)tiles[index].transform.localPosition.x;
            tiles[index].coords.z = (int)tiles[index].transform.localPosition.z;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
