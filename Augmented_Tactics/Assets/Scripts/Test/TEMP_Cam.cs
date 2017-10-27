﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_Cam : MonoBehaviour {

    private Rigidbody body;

    public float speed = 10f;
    public float angleDelta = 1f;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        CameraControls();

    }

    void CameraControls()
    {
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        Vector3 movement = (vert * transform.forward) + (hori * transform.right);

        body.velocity = movement * speed;

        if(Input.GetKey(KeyCode.Q))
        {
            body.transform.rotation = Quaternion.Euler(
                body.transform.rotation.eulerAngles.x,
                body.transform.rotation.eulerAngles.y + angleDelta,
                body.transform.rotation.eulerAngles.z
                );
        }
        if(Input.GetKey(KeyCode.E))
        {
            body.transform.rotation = Quaternion.Euler(
                body.transform.rotation.eulerAngles.x,
                body.transform.rotation.eulerAngles.y - angleDelta,
                body.transform.rotation.eulerAngles.z
                );
        }
    }
}
