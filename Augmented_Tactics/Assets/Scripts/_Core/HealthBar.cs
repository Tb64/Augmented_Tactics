using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private float healthPercent;
    private Transform mainCamera;
    private Transform healthbar;
     
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();
        healthbar = this.gameObject.transform.GetChild(0);
    }

    public void updateHealth(float health)
    {
        if (gameObject.transform.childCount == 2)
        {
            gameObject.transform.GetChild(1).transform.localScale = new Vector3(health, 1f, 1f);
        }
    }
        

// Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera);
        //healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();
        //Debug.Log(healthPercent);
        //transform.localScale = new Vector3(healthPercent, 1f, 1f);
    }

}

    
