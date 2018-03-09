using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosGenerator : MonoBehaviour {

    public GameObject tile;
    public Material white;
    public Material black;
    public int max_x;
    public int max_y;
    public bool generateMap;
    public bool showTileInGame;
    public string DEBUG;

    Material m_Material;

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

        //GenerateTiles();
    }

    public void GenerateTiles()
    {
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        if(loadedTiles != null)
        {
            DEBUG = "ABORTING!! TILES EXIST ALREADY!!";
            Debug.Log(DEBUG);
            return;
        }
        for (int x = 0; x < max_x; x++)
        {
            for (int z = 0; z < max_y; z++)
            {
                Vector3 localPos = new Vector3((float)x, 0f, (float)z);
                GameObject newtile = Instantiate(tile);
                newtile.transform.parent = transform;

                newtile.transform.localPosition = localPos;

                newtile.GetComponent<ClickableTile>().coords = localPos;

                Renderer m_render = newtile.GetComponent<Renderer>();
                if (((x + z) % 2) == 0)
                {
                    Debug.Log("setting black");
                    setMaterials(Color.black);
                }
                else
                {
                    setMaterials(Color.white);
                }
                if (showTileInGame)
                    m_render.enabled = true;
            }
        }
    }

    private void setMaterials(Color color)
    {
        Renderer rend = GetComponent<Renderer>();
        foreach (Material material in rend.materials)
        {
            material.color = color;
        }
    }

    public void ShowTiles()
    {
        DEBUG = "Showing Tiles";
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        foreach (ClickableTile tile in loadedTiles)
        {
            tile.GetComponent<Renderer>().enabled = true;
        }
    }

    public void HideTiles()
    {
        DEBUG = "Hiding Tiles";
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        foreach (ClickableTile tile in loadedTiles)
        {
            tile.GetComponent<Renderer>().enabled = false;
        }
    }
}
