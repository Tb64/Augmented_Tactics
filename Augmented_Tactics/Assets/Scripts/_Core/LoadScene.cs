using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    //Name of scene to load
    public string sceneName;
    public SceneLoader loader;
    bool sceneLoaded = false;
    int increment = 1;
    private void Awake()
    {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
    }

    private void Start()
    {
        increment = 1;
    }

    IEnumerator CoLoadNextScene()
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        //yield return new WaitForSeconds(5);
        //do
        //{
        //    Debug.Log("scene loading: " + op.progress);
        //    Debug.Log("isdone: " + op.isDone);
            
        //} while (op.isDone != true);
        op.allowSceneActivation = true;
        yield return op;

    }


    //will start thread that loads the scene
    private void loadScene()
    {
        //increment is used to prevent a scene being loaded more than once
        Debug.Log("Start Load");
        increment++;
        if (loader != null)
            loader.SceneLoad(sceneName);
        else
            StartCoroutine(CoLoadNextScene());
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player" && increment == 1)
                loadScene();
    }


}
