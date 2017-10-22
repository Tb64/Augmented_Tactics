using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosGenerator : MonoBehaviour {

	// Use this for initialization
	public void generatePositions() {
        ClickableTile[] tiles = GetComponentsInChildren<ClickableTile>();

        for(int index = 0; index < tiles.Length; index++)
        {
            tiles[index].coords.x = (int)tiles[index].transform.localPosition.x;
            tiles[index].coords.y = (int)tiles[index].transform.localPosition.y;
            tiles[index].coords.z = (int)tiles[index].transform.localPosition.z;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
