using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBattle : MonoBehaviour
{

    SceneManagement manager;

    private void Start()
    {
        if (GameObject.Find("SceneManager") != null)
        {
            manager = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        }
    }
   


private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //manager.LoadLevel(1);
        }
    }
}
