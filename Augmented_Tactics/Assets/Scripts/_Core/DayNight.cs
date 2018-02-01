using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNight : MonoBehaviour {

   
    GameObject sun;
    public float time;
    public TimeSpan currentTime;
    public Transform sunTransform;
    public Light Sun;
    //public Text timeText;
    public int hour;
    public int minute;

	// Use this for initialization
	void Start () {
       sun = GameObject.Find("Sun");
	}
	

	// Update is called once per frame
	void Update () {
    
	}
}
