using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private Image bar;
         
    void Start()
    {
        bar = this.GetComponent<Image>();
    }

// Edited by ivan
    void Update()
    {        
        //transform.localScale = new Vector3(
        //    (gameObject.GetComponentInParent<Actor>().GetHealthPercent()/100.0f), 1f, 1f)    
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

    
