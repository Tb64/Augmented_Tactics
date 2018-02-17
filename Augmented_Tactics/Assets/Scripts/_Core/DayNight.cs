using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DayNight : MonoBehaviour {

    public float time;
    public TimeSpan currentTime;
    public Transform sunTransform;
    public Transform moonTransform;
    public Light sun;
    public Light moon;
    public Text timeText;
    public int hour;
    public int minute;
    public int days;

    public float intensity;
    public Color fogDay = Color.grey;
    public Color fogNight = Color.black;

    public int speed;

    private void Start()
    {
        time = 30000;
    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if(time > 84600)
        {
            days += 1;
            time = 0;
        }
        currentTime = TimeSpan.FromSeconds(time);
        string[] tempTime = currentTime.ToString().Split(":" [0]);
        timeText.text = tempTime[0] + ":" + tempTime[1];
        moonTransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * -360, 0, 0));
        sunTransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * 360, 0, 0));
        if(time < 43200)
        {
            intensity = 1 - (43200 - time) / 43200
;       }
        else 
            intensity = 1 - ((43200 - time) / 43200 * -1);

        RenderSettings.fogColor = Color.Lerp(fogNight, fogDay, intensity * intensity);
        sun.intensity = intensity;
        moon.intensity = (1 - intensity) - .5f;
    }

   

	void Update ()
    {
        ChangeTime();
    }
}
