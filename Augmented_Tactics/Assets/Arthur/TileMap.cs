using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileMap : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;

    int[,] tiles;

    int mapSizeX = 64;
    int mapSizeY = 64;


    // Use this for initialization
    void Start() {
        //allocate tiles
        tiles = new int[mapSizeX, mapSizeY];

        //init tiles
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }

        }

        GenerateMap();

    }

    void GenerateMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickHandler ch = go.GetComponent<ClickHandler>();
                ch.tileX = x;
                ch.tileY = y;
                ch.map = this;
            }

        }
    }

    public void MoveSelectedUnitTo(int x, int y)
    {
        selectedUnit.transform.position = new Vector3(x, y, 0);
    }

}