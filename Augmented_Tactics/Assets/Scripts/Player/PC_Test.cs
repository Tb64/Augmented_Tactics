using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;


public class PC_Test : PlayerControlled
{

    public Animator animator;
    public NavMeshAgent agent;
    public float inputHoldDelay;

    private WaitForSeconds inputHoldWait;
    private Vector3 destinationPosition;

    // Use this for initialization
    void Start () {
        base.Start();
        agent.updateRotation = false;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        destinationPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        base.Start();
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;       //speed
    }
}
