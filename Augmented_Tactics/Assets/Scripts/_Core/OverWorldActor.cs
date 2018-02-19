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
    private Quaternion _lookRotation;
    private Vector3 _direction;
  
    public virtual void Start()
    {
        initialize();
    }

   
    public virtual void Update()
    {
        clickToMove();
        float angle = 0f;
        if (target.collider == true)
        {
            if (angle > 5f)
            {
                Vector3 targetDir = target.transform.position - transform.position;

            angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
            //find the vector pointing from our position to the target
            _direction = (target.transform.position - transform.position).normalized;
    
            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);
    
            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);

           
                transform.rotation = _lookRotation;
            }
        }
    }

    void initialize()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        playerAgent.updateRotation = false;
        rotationSpeed = 10f;

    }

    void clickToMove()
    {
        
        //playerAnim.SetFloat("Speed", playerAgent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0)) //&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
            rotateTowards();
        }
    }

    void rotateTowards()
    {
        Vector3 newDir;
       
            //Vector3 targetDir = target.transform.position - transform.position;
            float step = rotationSpeed * Time.deltaTime;
            var q = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

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
