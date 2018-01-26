using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour {
    public Slider slidy;

    void Start()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int sceneIndex)
    {

        StartCoroutine(LoadAsync(sceneIndex));

    }

    IEnumerator LoadAsync (int sceneIndex)
    {
      
        AsyncOperation loady = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loady.isDone)
        {
            float progress = Mathf.Clamp01(loady.progress / .9f);
            slidy.value = progress;

            yield return null;
        }
    }

}
