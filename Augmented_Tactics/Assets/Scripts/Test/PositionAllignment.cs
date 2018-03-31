using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAllignment : MonoBehaviour {

    public bool modelAsRoot = false;

    private Actor actor;

	// Use this for initialization
	void Start () {
        actor = GetComponent<Actor>();
        TurnBehaviour.OnActorFinishedMove += this.AlignUnit;
        TurnBehaviour.OnActorAttacked += this.AlignUnit;
    }
	

    void OnDestroy()
    {
        TurnBehaviour.OnActorFinishedMove -= this.AlignUnit;
        TurnBehaviour.OnActorAttacked -= this.AlignUnit;
    }

	void AlignUnit()
    {
        if (transform == null)
            return;

        if(modelAsRoot)
        {
            if(actor != null)
            {
                //transform.position = actor.getWorldCoords();
            }
        }
        else
        {
            Vector3 localPos = transform.localPosition;
            localPos.x = 0f;
            localPos.z = 0f;

            transform.localPosition = localPos;
        }

    }
}
