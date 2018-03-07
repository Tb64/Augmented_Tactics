using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverWorldActor : MonoBehaviour
{
    RaycastHit target;
    NavMeshAgent playerAgent;
    private Animator playerAnim;
    public float rotationSpeed;
    private Quaternion lookRotation;
    private Vector3 direction;
  
    public virtual void Start()
    {
        initialize();
    }

   
    public virtual void Update()
    {
        clickToMove();
        //if (target.collider != null) 
        //    rotateTowards();

    }

    void initialize()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        //playerAgent.updateRotation = false;
        rotationSpeed = 100f;
    }

    void clickToMove()
    {
        
        //playerAnim.SetFloat("Speed", playerAgent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0)) //&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
            
        }
    }

    void rotateTowards()
    {
        //Vector3 newDir = Vector3.RotateTowards(gameObject.transform.forward, target.transform.position, 1f, 0f);
        //newDir = new Vector3(newDir.x, gameObject.transform.position.y, newDir.z);

        //newDir = new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z);
        //gameObject.transform.LookAt(newDir);
        gameObject.transform.LookAt(target.transform);
    }

    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(interactionRay, out target, Mathf.Infinity))
        {
            GameObject interactedObject = target.collider.gameObject;
            if (interactedObject.tag == "Interactable")
            {
                Debug.Log("Interactable object");
            }
            else
            {
                //move our player to the point
                playerAgent.destination = target.point;
               
            }
        }
    }
}
