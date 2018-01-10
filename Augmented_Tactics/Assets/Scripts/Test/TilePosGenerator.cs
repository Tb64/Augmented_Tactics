using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosGenerator : MonoBehaviour {

    public GameObject tile;
    public int max_x;
    public int max_y;
    public bool generateMap;

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

    private void Start()
    {
        if (!generateMap)
            return;

        for (int x = 0; x < max_x; x++)
        {
            for (int z = 0; z < max_y; z++)
            {
                Vector3 localPos = new Vector3((float)x, 0f,(float)z);
                GameObject newtile = Instantiate(tile);
                newtile.transform.parent = transform;
                
                newtile.transform.localPosition = localPos;

                newtile.GetComponent<ClickableTile>().coords = localPos;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
