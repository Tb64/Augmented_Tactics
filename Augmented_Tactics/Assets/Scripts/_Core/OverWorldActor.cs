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
  
    public virtual void Start()
    {
        initialize();
    }

   
    public virtual void Update()
    {
        clickToMove();
        if(target.collider == true)
            rotateTowards();
    }

    void initialize()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        playerAgent.updateRotation = false;
        rotationSpeed = 1f;
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
        Vector3 newDir;
       
            Vector3 targetDir = target.transform.position - transform.position;
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform, step);

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
