using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHighlight : MonoBehaviour {

    public GameObject hightlightObj;
    public int range;
    private TileMap map;
	// Use this for initialization
	void Start () {
        map = GameObject.Find("Map").GetComponent<TileMap>();
        //Marker_On(transform.position,3);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Marker_On(Vector3 positionInput, int range)
    {
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
                    if(map.UnitCanEnterTile((int)spawnPosition2.x, (int)spawnPosition2.z))
                    {
                        obj = Instantiate(hightlightObj, map.TileCoordToWorldCoord(spawnPosition2), hightlightObj.transform.rotation);
                        obj.transform.parent = gameObject.transform;
                    }
                }
                if (map.UnitCanEnterTile((int)spawnPosition1.x, (int)spawnPosition1.z))
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
}
