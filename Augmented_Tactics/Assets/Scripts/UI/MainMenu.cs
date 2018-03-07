using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public void loadHub()
    {
        SceneManager.LoadSceneAsync("Castle_Hub");
    }

}
