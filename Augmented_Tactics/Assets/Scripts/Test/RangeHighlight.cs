﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHighlight : MonoBehaviour {

    public GameObject hightlightObj;
    public int range;
    private TileMap map;
	// Use this for initialization
	void Start () {
        TurnBehaviour.OnUnitMoved += this.MoveFinished;

        map = GameObject.Find("Map").GetComponent<TileMap>();
        //Marker_On(transform.position,3);
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnUnitMoved -= this.MoveFinished;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MoveFinished()
    {
        Marker_Off();
    }

    public void Move_Marker_On(Vector3 positionInput, int range)
    {
        Marker_Off();
        TileMap map = GameObject.Find("Map").GetComponent<TileMap>();
        int rangeDelta = range;
        for (int x = 0; x <= range; x++)
        {
            for (int z = -rangeDelta; z <= rangeDelta; z++)
            {
                Vector3 spawnPosition1 = new Vector3(positionInput.x + x, positionInput.y, positionInput.z + z);
                Vector3 spawnPosition2 = new Vector3(positionInput.x - x, positionInput.y, positionInput.z + z);
                GameObject obj;
                if (spawnPosition1 != spawnPosition2)
                {
                    if(map.UnitCanEnterTile(spawnPosition2))
                    {
                        obj = Instantiate(hightlightObj, map.TileCoordToWorldCoord(spawnPosition2), hightlightObj.transform.rotation);
                        obj.transform.parent = gameObject.transform;
                    }
                }
                if (map.UnitCanEnterTile(spawnPosition1))
                {
                    obj = Instantiate(hightlightObj, map.TileCoordToWorldCoord(spawnPosition1), hightlightObj.transform.rotation);
                    obj.transform.parent = gameObject.transform;
                }
            }
            rangeDelta--;
        }
    }

    public void Attack_Marker_On(Vector3 positionInput, int rangeMin, int rangeMax)
    {
        Marker_Off();
        GameObject obj;
        TileMap map = GameObject.Find("Map").GetComponent<TileMap>();
        int rangeDelta = rangeMax;
        for (int x = rangeMin; x <= rangeMax; x++)
        {
            for (int z = -rangeDelta; z <= rangeDelta; z++)
            {
                Vector3 spawnPosition1 = new Vector3(positionInput.x + x, positionInput.y, positionInput.z + z);
                Vector3 spawnPosition2 = new Vector3(positionInput.x - x, positionInput.y, positionInput.z + z);
                if (spawnPosition1 != spawnPosition2)
                {
                    if (map.IsValidCoord(spawnPosition2))
                    {
                        obj = Instantiate(hightlightObj, map.TileCoordToWorldCoord(spawnPosition2), hightlightObj.transform.rotation);
                        obj.transform.parent = gameObject.transform;
                    }
                }
                if (map.IsValidCoord(spawnPosition1))
                {
                    obj = Instantiate(hightlightObj, map.TileCoordToWorldCoord(spawnPosition1), hightlightObj.transform.rotation);
                    obj.transform.parent = gameObject.transform;
                }
            }
            rangeDelta--;
        }
    }

    public void Marker_Off()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach(Transform child in children)
        {
            if(child.name.Contains("Cube"))
                Destroy(child.gameObject);
        }
    }
    public bool IsValidPosition(Vector3 input)
    {
        int max_X = 1000;
        int max_Y = 1000;
        int max_Z = 1000;

        if (map != null)
        {
            max_X = map.mapSizeX;
            max_Y = map.mapSizeX;
            max_Z = map.mapSizeZ;
        }

        if (input.x < 0)
            return false;
        if (input.y < 0)
            return false;
        if (input.z < 0)
            return false;


        return true;
    }
}
