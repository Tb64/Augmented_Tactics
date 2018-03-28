using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2CameraFollow : MonoBehaviour {
    public Transform target;
    public float smoothSpeed = 0.0125f;
    public Vector3 offset;
	


	void LateUpdate() {
        transform.position = target.position + offset;
	}
}
