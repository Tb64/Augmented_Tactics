using UnityEngine.Audio;
using System;
using UnityEngine;

public class SFXController : MonoBehaviour {

    public Sfx[] sounds;


	// Use this for initialization
	void Awake() {
		foreach (Sfx s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }

	}
	
	public void Play (string name)
    {
        Sfx s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
