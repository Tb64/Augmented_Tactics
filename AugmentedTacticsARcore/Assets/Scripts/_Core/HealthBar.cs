using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private float healthPercent;
    private Transform mainCamera;
     
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();

    }

// Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera);
        //healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();
        //transform.localScale = new Vector3(healthPercent, 1f, 1f);
    }

}

    
