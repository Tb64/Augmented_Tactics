using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour {
    public GameObject[] objects;
    int index = 0;
	// Use this for initialization
	void Start () {
        objects[index].SetActive(true);
	}
	
	public void NextObjectLoad()
    {
        index++;
        if(index == 3)
        {
            GameObject obj = GameObject.Find("Skip Canvas");
            obj.SetActive(false);
        }
        if(index > objects.Length || index < 0)
        {
            Debug.Log("Scene Not Loaded");
            return;
        }
        DisableAll();
        objects[index].SetActive(true);
    }
    private void DisableAll()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

    }
}
