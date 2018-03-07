using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTavern : MonoBehaviour {

    //Temporary script. Will be replaced with a manager that uses raycasts
    //to detect which scene to load

    SceneManagement manager;

    private void Start()
    {
        if (GameObject.Find("SceneManager") != null)
        {
            manager = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        }
    }

    private void OnMouseDown()
    {
        //manager.LoadLevel(2);
    }

    private void OnColliderEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //manager.LoadLevel(2);
        }
    }
}
