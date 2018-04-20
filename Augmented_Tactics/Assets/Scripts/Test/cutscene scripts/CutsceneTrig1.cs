using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrig1 : MonoBehaviour
{

    public bool exit = false;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        exit = true;
        GameObject go = GameObject.Find("Cutscene Controller");
        Cutscene1 test = (Cutscene1)go.GetComponent(typeof(Cutscene1));
        test.SetDiagStart();
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Still There");
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        
    }
}