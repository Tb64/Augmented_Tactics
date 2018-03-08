using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;

    public void SceneLoad(string levelname)
    {
        StartCoroutine(SceneLoadAsync(levelname));
    }

    IEnumerator SceneLoadAsync(string levelname)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelname);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            Debug.Log("Loading " + levelname + ": " + progress);

            yield return null;
        }
    }
}
