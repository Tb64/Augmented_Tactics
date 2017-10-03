using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

<<<<<<< HEAD
    private Image bar;
         
    void Start()
    {
        bar = this.GetComponent<Image>();
=======
    private float healthPercent;
    private Transform mainCamera;
     
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();

>>>>>>> master
    }

// Edited by ivan
    void Update()
<<<<<<< HEAD
    {        
        //transform.localScale = new Vector3(
        //    (gameObject.GetComponentInParent<Actor>().GetHealthPercent()/100.0f), 1f, 1f)    
=======
    {
        transform.LookAt(mainCamera);
        //healthPercent = gameObject.GetComponentInParent<Actor>().GetHealthPercent();
        //transform.localScale = new Vector3(healthPercent, 1f, 1f);
>>>>>>> master
    }

    public void UpdateUIHealth(float amount, bool isPlayer)
    {
        if (PlayerControlled.playerList != null && isPlayer)
            foreach (Actor a in PlayerControlled.playerList)
                bar.fillAmount = amount;

        if (Enemy.enemyList!=null && !isPlayer)
            foreach (Actor a in Enemy.enemyList)
                transform.localScale = new Vector3((amount), 1f, 1f);
    }
}

    
