using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesAnimationTigger : MonoBehaviour {
    private Button Ability1;
    private Button Ability2;
    private Button Ability3;
    private Button Ability4;
    private Animator animator;

    // Use this for initialization
    void Start () {

        Button[] temp = GetComponentsInChildren<Button>();
        animator = GameObject.FindGameObjectWithTag("UI").GetComponent<Animator>();

        if (temp != null)
        {
            foreach (Button button in temp)
            {
                switch (button.name)
                {
                    case "Ability1":
                        Ability1 = button;
                        Ability1.onClick.AddListener(clickedAbility);
                        break;
                    case "Ability2":
                        Ability2 = button;
                        Ability2.onClick.AddListener(clickedAbility);
                        break;
                    case "Ability3":
                        Ability3 = button;
                        Ability3.onClick.AddListener(clickedAbility);
                        break;
                    case "Ability4":
                        Ability4 = button;
                        Ability4.onClick.AddListener(clickedAbility);
                        break;
                }
            }
        }

    }

    private void clickedAbility()
    {
        if (animator != null)
            animator.SetTrigger("");
    }
}
