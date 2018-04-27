using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour {
    public float speed = 10f;
    public string targetName;
	// Use this for initialization
	void Start () {
        Rigidbody rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            return;

        rbody.velocity = transform.forward * speed; //adding forward velocity
        Destroy(gameObject, 10f);  //destory the arrow after 10f
	}

    public void SetTarget(string input)
    {
        targetName = input;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == targetName)
        {
            Debug.Log("Arrow hit " + other.name);
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
