using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

    public Text buttonText;
    private int sceneIndex;
    private string sceneName;

    public void ButtonClicked()
    {
        Debug.Log("Setting: " + sceneIndex + " " + sceneName);
        SendMessageUpwards("LevelButtonClicked", sceneIndex);
    }

    public void Init(string label, int index)
    {
        if (buttonText != null)
            buttonText.text = label;
        sceneIndex = index;
        sceneName = label;
    }

    public void Init(int index)
    {
        string name = SceneUtility.GetScenePathByBuildIndex(index);
        sceneName = name;
        Init(name, index);
    }
}
