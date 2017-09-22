using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private float healthPercent;

     
    void Start()
    {
        healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();
    }

// Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(healthPercent, 1f, 1f);
    }

}

    
