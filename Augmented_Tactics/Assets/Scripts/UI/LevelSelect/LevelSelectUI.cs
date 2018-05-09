using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour {

    public GameObject buttonContainer;
    public GameObject buttonPrefab;

    public static string[] levelDisplayNames =
    {
        "Main Menu",
        "Foward Operating Base",
        "Wedding Day",
        "Locked",
        "Locked",
        "Locked",
        "Locked",
        "Locked",
        "Locked",
        "Locked"
    };

    // Use this for initialization
    void Start () {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (sceneCount >= 2)
        {
            buttonPrefab.GetComponent<LevelButton>().Init(levelDisplayNames[2], 2);
            buttonPrefab.GetComponent<Button>().onClick.AddListener(buttonPrefab.GetComponent<LevelButton>().ButtonClicked);
        }
        else
            return;
        Vector3 pos = buttonPrefab.GetComponent<RectTransform>().anchoredPosition3D;
        Debug.Log(buttonContainer.GetComponent<RectTransform>().rect);
        for (int index = 3; index < sceneCount; index++)
        {
            pos.y -= 100;
            GameObject obj = Instantiate<GameObject>(buttonPrefab);
            obj.transform.SetParent(buttonContainer.transform, false);
            obj.GetComponent<Button>().onClick.AddListener(obj.GetComponent<LevelButton>().ButtonClicked);
            obj.GetComponent<RectTransform>().anchoredPosition3D = pos;
            obj.GetComponent<LevelButton>().Init(levelDisplayNames[index],index);
        }
        RectTransform rect = buttonContainer.GetComponent<RectTransform>();
        //rect.rect.height = (sceneCount - 2) * 100;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
