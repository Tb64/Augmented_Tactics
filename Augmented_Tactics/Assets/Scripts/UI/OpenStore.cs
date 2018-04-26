using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStore : MonoBehaviour {

    public GameObject store;
    public GameObject storeUI;
    GameObject RayCaster()
    {
        GameObject interactedObject;
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            interactedObject = interactionInfo.collider.gameObject;
            Debug.Log("Click event on: " + interactedObject.name);
            return interactedObject;
        }
        return null;
    }

    private void OnMouseUp()
    {
        RayCaster();
        if(storeUI.activeSelf == false)
        {
            storeUI.SetActive(true);
        }

        //store.GetComponentInChildren<Store>().toggleInventory();
        Debug.Log("OPENING STORE");
    }

   
}
