using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    public void LoadHub()
    {//loads main hub
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

}
