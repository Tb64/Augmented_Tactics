using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverCalculator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float ValidCover(Actor attacker, Actor defender)
    {
        Vector3 defCoords = defender.getCoords();
        Vector3 atkCoords = attacker.getCoords();
        Vector3 delta = defCoords - atkCoords;

        if(delta.x == 0f)
        {

        }
        return 0f;
    }

    public Vector3 hasCover(Actor defender)
    {
        TileMap map = defender.map;
        Vector3 defCoords = defender.getCoords();
        Vector3 delta = new Vector3(1f, 1f, 1f);
        if(map.getTileAtCoord(defCoords + delta))
        {

        }

        return new Vector3();
    }
}
