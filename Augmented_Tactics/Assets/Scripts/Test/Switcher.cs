using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour {

    public GameObject[] objects;

    private int currentIndex = 0;

	// Use this for initialization
	void Start () {
        objects[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextObjectOn()
    {
        currentIndex++;
        if (currentIndex < 0 || currentIndex >= objects.Length)
            return;
        DisableAll();
        objects[currentIndex].SetActive(true);
    }

    private void DisableAll()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
