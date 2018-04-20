using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenetrigger6 : MonoBehaviour
{

    public bool exit = false;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        GameObject go = GameObject.Find("Cutscene Controller");
        Cutscene6 test = (Cutscene6)go.GetComponent(typeof(Cutscene6));
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