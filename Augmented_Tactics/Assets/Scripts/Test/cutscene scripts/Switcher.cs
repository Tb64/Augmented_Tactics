using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;
using UnityEngine.Playables;

public class Switcher : MonoBehaviour {
    public GameObject[] objects;
    int index = 0;
	// Use this for initialization
	void Start () {
        objects[index].SetActive(true);
	}
	
	public void NextObjectLoad()
    {
        ExampleDialogueUI.ResetLineCount();
        index++;
        if(index == 3 || objects[1].name == "Level1TestPrefab")
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
