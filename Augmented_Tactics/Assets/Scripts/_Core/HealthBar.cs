using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Actor unit;
     
    void Start()
    {
        
    }

// Edited by ivan
    void Update()
    {        
        //transform.localScale = new Vector3(
        //    (gameObject.GetComponentInParent<Actor>().GetHealthPercent()/100.0f), 1f, 1f)    
    }

    public void UpdateUIHealth(float amount)
    {
        transform.localScale = new Vector3((amount), 1f, 1f);
    }
}

    
