using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mouseRotateSpeed = 0.001f;

    private Animator animator;
    private Quaternion initRotation;

    private bool startMouseRotate;
    private Vector3 prevMousePosition;

	void Awake ()
	{
	    animator = GetComponent<Animator>();
	    initRotation = transform.rotation;
	}
	
	void Update () {
	    if (!animator.GetBool("jumpComplete"))
	        return;

	    if (Input.GetMouseButtonDown(1))
	    {
	        startMouseRotate = true;
	        prevMousePosition = Input.mousePosition;
	    }
	    if (Input.GetMouseButtonUp(1)) {
	        startMouseRotate = false;
	    }
	    if (Input.GetMouseButton(1))
	    {
            transform.Rotate(new Vector3(0, (Input.mousePosition.x-prevMousePosition.x) * mouseRotateSpeed , 0));
	        prevMousePosition = Input.mousePosition;
        }

        if (Input.GetButtonDown("Jump"))
	    {
	        animator.SetTrigger("jump");
	        animator.SetBool("jumpComplete", false);
	    }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

	    if (Mathf.Abs(h) > 0.001f)
	        v = 0;


	    if (!startMouseRotate)
	    {
	        if (h > 0.5f)
	        {
	            transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, -90, 0));
	        }
	        if (h < -0.5f)
	        {
	            transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, 90, 0));
	        }
	        if (v > 0.5f)
	        {
	            transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, -180, 0));
	        }
	        if (v < -0.5f)
	        {
	            transform.rotation = Quaternion.Euler(initRotation.eulerAngles);
	        }
	    }

	    animator.SetFloat("speed", Mathf.Max(Mathf.Abs(h), Mathf.Abs(v)));

    }
}
