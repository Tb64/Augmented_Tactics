using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

    public Text buttonText;
    private int sceneIndex;

    public void ButtonClicked()
    {
        SendMessageUpwards("LevelButtonClicked", sceneIndex);
    }

    public void Init(string label, int index)
    {
        if (buttonText != null)
            buttonText.text = label;
        sceneIndex = index;
    }

    public void Init(int index)
    {
        string name = SceneUtility.GetScenePathByBuildIndex(index);
        Init(name, index);
    }
}
