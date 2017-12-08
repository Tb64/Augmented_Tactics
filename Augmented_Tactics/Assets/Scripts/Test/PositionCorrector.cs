using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCorrector : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TurnBehaviour.OnActorAttacked += this.PositionCorrect;
        TurnBehaviour.OnActorMoved += this.PositionCorrect;
        TurnBehaviour.OnUnitMoved += this.PositionCorrect;
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnActorAttacked -= this.PositionCorrect;
        TurnBehaviour.OnActorMoved -= this.PositionCorrect;
        TurnBehaviour.OnUnitMoved -= this.PositionCorrect;

    }

    void PositionCorrect()
    {
        Vector3 newPos = transform.localPosition;
        newPos.x = 0f;
        newPos.z = 0f;

        transform.localPosition = newPos;
    }
}
