using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class WeddingTrigger : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entered");
        GameObject go = GameObject.Find("Cutscene Controller");
        Wedding test = (Wedding)go.GetComponent(typeof(Wedding));
        test.EndScene();
    }
}