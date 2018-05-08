using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class CutsceneTrig1 : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public ExampleDialogueUI diagscript;


    public bool exit = false;
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entered");
        cam1.enabled = false;
        cam2.enabled = true;
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
        diagscript.DialogueComplete();

    }
}