using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour {

    //Name of scene to load
    public string sceneName;
    bool sceneLoaded = false;
    int increment = 1;

    private void Start()
    {
        increment = 1;
    }

    IEnumerator CoLoadNextScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        yield return op;
        op.allowSceneActivation = true;

    }

    //will start thread that loads the scene
    private void loadScene()
    {
        //increment is used to prevent a scene being loaded more than once
        increment++;    
        StartCoroutine(CoLoadNextScene());
    }
  
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player" && increment == 1)
            loadScene();
    }


}
