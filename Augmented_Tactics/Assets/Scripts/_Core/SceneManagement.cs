using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    public static void LoadHub()
    {//loads main hub
        SceneManager.LoadSceneAsync("castle_hub");
    }

}
