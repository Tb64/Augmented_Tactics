using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public SceneLoader loader;

	public void loadHub()
    {
        if (loader != null)
            loader.SceneLoad("Castle_Hub");
        else
         SceneManager.LoadSceneAsync("Castle_Hub");
    }

}
