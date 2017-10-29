using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour {

    public Animator animator;
    private Text damageText;
    private Color32 textColor;
    
	void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        damageText = animator.GetComponent<Text>();
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void setText(string text, Color32 textColor)
    {
        damageText.text = text;
        damageText.GetComponentInChildren<Text>().color = textColor;
    }

   
	
}
