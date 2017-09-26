using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private float healthPercent;

     
    void Start()
    {
        
    }
// Edited by ivan
// Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            (gameObject.GetComponentInParent<Actor>().GetHealthPercent()/100.0f), 1f, 1f);
    }

}

    
