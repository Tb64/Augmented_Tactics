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
    private Vector2 lastClickPosition;
    private float touchStart;
    public const float touchHoldTreshold = 0.5f;
    public static float touchDistTreshold = 10f;

    public virtual void Start()
    {
        initialize();
    }

   
    public virtual void Update()
    {
        clickToMove();
        TouchEvent();
        playerAnim.SetFloat("Speed", gameObject.GetComponent<NavMeshAgent>().velocity.magnitude);

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
        if (Input.touchCount > 0)
            return;
        //playerAnim.SetFloat("Speed", playerAgent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
            
        }
    }

    void TouchEvent()
    {
        if (Input.touchCount > 0)// && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
        }
        //float distance;
        if (Input.touchCount == 1)
        {
            Touch touch1 = Input.GetTouch(0);
            lastClickPosition = touch1.position;
            if (touch1.phase == TouchPhase.Began)
                touchStart = Time.time;

            float touchDuration = Time.time - touchStart;

            if (touch1.phase == TouchPhase.Ended)
            {
                if (touch1.deltaPosition.magnitude < touchDistTreshold && touchDuration < touchHoldTreshold)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        GetInteraction();
                    }
                }
            }
        }
    }

    void rotateTowards()
    {
       
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
