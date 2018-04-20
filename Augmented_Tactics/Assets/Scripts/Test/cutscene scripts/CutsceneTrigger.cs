using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
    
    public bool exitedTrig = false;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Still There");
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        exitedTrig = true;
        GameObject go = GameObject.Find("Cutscene Controller");
        Cutscene2 test = (Cutscene2)go.GetComponent(typeof(Cutscene2));
        test.StopWalking();
    }
}
