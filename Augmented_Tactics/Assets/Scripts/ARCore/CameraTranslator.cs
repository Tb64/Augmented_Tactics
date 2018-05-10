﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class CameraTranslator : MonoBehaviour {

    public GameObject arCam;
    public GameObject viewCam;

    public Vector3 restDelta;

    private Vector3 startingDelta;

    private void Awake()
    {
        int AREnabled = PlayerPrefs.GetInt("AREnabled", 0);
        if (AREnabled == 0)
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
        startingDelta = viewCam.transform.localPosition;
        if(arCam == null)
        {
            arCam = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
	
	// Update is called once per frame
	void Update () {
        DebugMobile.Log("ar Cam: " + arCam.transform.position + " viewCam: " + viewCam.transform.localPosition + " sD: " + startingDelta);
        viewCam.transform.localPosition = (arCam.transform.position * 10f) + startingDelta;
        viewCam.transform.localRotation = arCam.transform.rotation;
	}

    public void ResetAR()
    {
        DebugMobile.Log("Reset Camera view");
        viewCam.transform.localPosition = restDelta;
        startingDelta = restDelta - (arCam.transform.position * 10f);
    }
}
