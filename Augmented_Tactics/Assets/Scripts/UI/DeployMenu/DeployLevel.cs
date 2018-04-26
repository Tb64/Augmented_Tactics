using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployLevel : MonoBehaviour {

    private int nextScene;

    public SceneLoader sceneloader;
    public DeployController deployUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelButtonClicked(int index)
    {
        nextScene = index;
    }

    public void LevelSelected()
    {
        //deployUI.gameObject.SetActive(true);
    }

    public void LoadLevel()
    {
        sceneloader.SceneLoad(nextScene);
    }
}
