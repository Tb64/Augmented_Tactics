using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour {

    public GameObject buttonContainer;
    public GameObject buttonPrefab;

    public static string[] levelDisplayNames =
    {
        "",
        "",
        "",
        "",
        "",
        ""
    };

    // Use this for initialization
    void Start () {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (sceneCount >= 2)
            buttonPrefab.GetComponent<LevelButton>().Init(2);
        else
            return;
        Vector3 pos = buttonPrefab.GetComponent<RectTransform>().anchoredPosition3D;
        Debug.Log(buttonContainer.GetComponent<RectTransform>().rect);
        for (int index = 3; index < sceneCount; index++)
        {
            pos.y -= 100;
            GameObject obj = Instantiate<GameObject>(buttonPrefab);
            obj.transform.SetParent(buttonContainer.transform, false);
            obj.GetComponent<RectTransform>().anchoredPosition3D = pos;
            obj.GetComponent<LevelButton>().Init(index);
        }
        RectTransform rect = buttonContainer.GetComponent<RectTransform>();
        //rect.rect.height = (sceneCount - 2) * 100;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelButtonClicked(int index)
    {

    }
}
