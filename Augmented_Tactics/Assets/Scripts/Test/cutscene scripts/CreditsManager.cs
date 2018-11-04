using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour {

    public int startingCamera;
    public Actor[] actors; //azeem, andrew, athur, gregg, justin, ivan, tuan
    public GameObject[] cameras;
    public string[] animationStateNames;
    public string[] cameraAnimationTriggers;
    public float[] animationTimes;
    public string[] text;
    public Text[] textBoxes;
    public int[] textLocation;
    private int iterator;

    // Use this for initialization
    void Start () {
        iterator = 0;
        StartCoroutine(Sequence());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Sequence()
    {
        //prime animations
        foreach(Actor actor in actors)
        {
            if (actor != null)
            {
                actor.GetComponent<Animator>().Play(animationStateNames[iterator], 0, animationTimes[iterator]);//("Unarmed-Attack-R2", 0, 0.366f);
                actor.GetComponent<Animator>().speed = 0;
            }
            iterator++;
        }
        iterator = 0;

        //play cameras
        foreach (GameObject camera in cameras)
        {
            //used to skip cameras for debug
            if (startingCamera-- > 0 && camera != null)
            {
                cameras[iterator + 1].SetActive(true);
                cameras[iterator].SetActive(false);
            }
            else
            {
                textBoxes[textLocation[iterator]].text = text[iterator];
                camera.GetComponent<Animator>().SetTrigger(cameraAnimationTriggers[iterator]);
                yield return new WaitForSeconds(7);
                cameras[iterator + 1].SetActive(true);
                cameras[iterator].SetActive(false);
                textBoxes[textLocation[iterator]].text = "";
            }
            iterator++;
        }       
    }

}
