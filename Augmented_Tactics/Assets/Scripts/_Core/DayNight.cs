﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNight : MonoBehaviour {

    DateTime time;
    GameObject sun;
    public int hour;
    public int minute;

	// Use this for initialization
	void Start () {
       sun = GameObject.Find("Sun");
	}
	
    public int twelveHour()
    {
        hour = time.Hour;

        if(hour > 12)
        {
            hour = hour - 12;
        }

        return hour;
    }

    void dayCycle()
    {
        //Color light = sun.GetComponent<Color>();

        if (time.Hour >= 18)
        {
            sun.GetComponent<Light>().color = new Color(0, 0, 0, 255);
        }

        if (time.Hour >= 6 && time.Hour < 18)
        {
            sun.GetComponent<Light>().color = new Color(255, 243, 210, 255);
        }
    }

	// Update is called once per frame
	void Update () {
        time = DateTime.Now;
        twelveHour();
        dayCycle();
        minute = time.Minute;
	}
}
