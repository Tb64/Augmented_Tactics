using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Slider slider;
    public Button cancelButton;
    public Button deleteSaveButton;
    public Toggle arToggle;

    private float previousVolume;
    private static string key = "volume";
    
    //this happens BEFORE start, but needed because it goes off every time the object is enabled
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey(key))
            previousVolume = 0.5f;
        else
            previousVolume = PlayerPrefs.GetFloat(key);

        if (PlayerPrefs.GetInt("AREnabled", 0) == 0)
            arToggle.isOn = false;
        else
            arToggle.isOn = true;
    }

    private void Start()
    {
        //setup for volume slider
        if (!PlayerPrefs.HasKey(key))
            slider.value = 0.5f;
        else
            slider.value = PlayerPrefs.GetFloat(key);

        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        cancelButton.onClick.AddListener(Cancel);
        deleteSaveButton.onClick.AddListener(DeleteSave);
    }

    void ValueChangeCheck()
    {
        PlayerPrefs.SetFloat(key, slider.value);
        AudioListener.volume = slider.value;
    }

    public void ToggleAR()
    {
        if (arToggle.isOn)
            PlayerPrefs.SetInt("AREnabled", 1);
        else
            PlayerPrefs.SetInt("AREnabled", 0);
    }

    void Cancel()
    {
        AudioListener.volume = previousVolume;
        PlayerPrefs.SetFloat(key, previousVolume);
        slider.value = previousVolume;
        this.gameObject.SetActive(false);
    }

    void DeleteSave()
    {
        /*GameDataController.gameData = new GameData();
        GameDataController.savePlayerData();*/
        Debug.Log("delete is fucked");
    }

    //if cancel button is set, AudioListener.volume = previousVolume; PlayerPrefs.SetFloat(key, previousVolume); Disable OptionsGameObj;

    public static string Key
    {
        get { return key.ToString(); }
    }
}
