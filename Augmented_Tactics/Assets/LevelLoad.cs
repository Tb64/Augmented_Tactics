using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour {
    public GameObject canvasObject;
    public Slider slidy;
    public GameObject background;

    void Start()
    {
        DisableCanvas();
    }

    private void DisableCanvas()
    {
        canvasObject.GetComponent<Canvas>().enabled = false;
    }
    private void EnableCanvas()
    {
        canvasObject.GetComponent<Canvas>().enabled = true;
    }

    public void StartLoad(int sceneIndex)
    {
        Debug.Log("load actually started");
        EnableCanvas();
        StartCoroutine(LoadAsync(sceneIndex));

    }

    

    IEnumerator LoadAsync (int sceneindex)
    {
      
        AsyncOperation loady = SceneManager.LoadSceneAsync(sceneindex);

        while (!loady.isDone)
        {
            float progress = Mathf.Clamp01(loady.progress / .9f);
            slidy.value = progress;

            yield return null;
        }
    }

}
