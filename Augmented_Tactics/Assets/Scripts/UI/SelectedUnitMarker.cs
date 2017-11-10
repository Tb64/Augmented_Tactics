using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;

public class SelectedUnitMarker : MonoBehaviour {


    public void AttachMarker(Actor SelectedUnit)
    {
        DetachMarker();
        AttachMarker(SelectedUnit.gameObject);
    }

    public void AttachMarker(GameObject SelectedUnit)
    {

        Debug.Log(" HELLO I AM A UNIT ATTACH LOL ^.^");
        DetachMarker();
        transform.Find("Cube").GetComponent<MeshRenderer>().enabled = true;
        transform.position = SelectedUnit.transform.position;
        transform.parent = SelectedUnit.transform;
    }

    public void DetachMarker()
    {
        transform.parent = null;
        transform.Find("Cube").GetComponent < MeshRenderer >().enabled = false;
    }

}
