using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovemnt : MonoBehaviour {

    NavMeshAgent playerAgent;
    private Animator playerAnim;

    // Use this for initialization
    void Start () {
        initialize();

    }
	
	// Update is called once per frame
	void Update () {
        clickToMove();
        playerAnim.SetFloat("Speed", playerAgent.velocity.magnitude);
    }

    void initialize()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
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
