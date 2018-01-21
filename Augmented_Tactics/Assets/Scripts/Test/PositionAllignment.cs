using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAllignment : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TurnBehaviour.OnUnitMoved += this.AlignUnit;
        TurnBehaviour.OnActorAttacked += this.AlignUnit;
    }
	
	void AlignUnit()
    {
        Vector3 localPos = transform.localPosition;
        localPos.x = 0f;
        localPos.z = 0f;

        transform.localPosition = localPos;
    }
}
