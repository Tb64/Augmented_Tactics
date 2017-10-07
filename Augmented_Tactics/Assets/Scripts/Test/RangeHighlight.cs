using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHighlight : MonoBehaviour {

    public GameObject hightlightObj;

    public int range;
	// Use this for initialization
	void Start () {
        RangeMarker_On(transform.position,3);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void RangeMarker_On(Vector3 positionInput, int range)
    {
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
                    obj = Instantiate(hightlightObj, spawnPosition2, hightlightObj.transform.rotation);
                    obj.transform.parent = gameObject.transform;
                }

                obj = Instantiate(hightlightObj, spawnPosition1, hightlightObj.transform.rotation);
                obj.transform.parent = gameObject.transform;
            }
            rangeDelta--;
        }
    }

    void RangeMarker_Off()
    {
        Destroy(gameObject);
    }
}
