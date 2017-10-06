using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovemnt : MonoBehaviour {

    NavMeshAgent playerAgent;

    // Use this for initialization
    void Start () {
        playerAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        clickToMove();
	}



    void clickToMove()
    {
        if (Input.GetMouseButtonDown(0) &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
    }

    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Interactable")
            {
                Debug.Log("Interactable object");
            }
            else
            {
                //move our player to the point
                playerAgent.destination = interactionInfo.point;
            }
        }
    }

}
