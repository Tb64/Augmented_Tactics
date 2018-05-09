using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //if the key exists, volume is set at the player settings, else use another settings
        if (!PlayerPrefs.HasKey(OptionsMenu.Key))
            AudioListener.volume = 0.5f;
        else
            AudioListener.volume = PlayerPrefs.GetFloat(OptionsMenu.Key);
    }
}
