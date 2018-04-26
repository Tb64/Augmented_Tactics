﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private bool cancelButtonUp;
    public Animator animator;
    private GameObject move;
    private GameObject skills;
    private GameObject end;
    private GameObject cancel;

    /// <summary>
    /// These are the 4 buttons overlaying the health bar circles.
    /// </summary>
    private Button[] selectButtons;
    
    //must be Awake or else if a unit spawns before this activates, some select buttons may be disabled
	void Awake ()
    {
        cancelButtonUp = false;
        selectButtons = new Button[4];
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Button");
        if (temp != null)
        {
            foreach (GameObject button in temp)
            {
                switch (button.name)
                {
                    case "MoveButton":
                        move = button;
                        break;
                    case "SkillsButton":
                        skills = button;
                        break;
                    case "EndButton":
                        end = button;
                        break;
                    case "CancelActionButton":
                        cancel = button;
                        break;
                    case "SelectButton1":
                        selectButtons[0] = button.GetComponent<Button>();
                        selectButtons[0].onClick.AddListener(OnClick0);
                        break;
                    case "SelectButton2":
                        selectButtons[1] = button.GetComponent<Button>();
                        selectButtons[1].onClick.AddListener(OnClick1);
                        break;
                    case "SelectButton3":
                        selectButtons[2] = button.GetComponent<Button>();
                        selectButtons[2].onClick.AddListener(OnClick2);
                        break;
                    case "SelectButton4":
                        selectButtons[3] = button.GetComponent<Button>();
                        selectButtons[3].onClick.AddListener(OnClick3);
                        break;
                }
            }
        }

        //disable while moving, on enemy turn start, and if default unit is out of actions. Try to enable when switching
        //to new unit
        TurnBehaviour.OnPlayerStartMove += disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd += disableActionsB;
        TurnBehaviour.OnPlayerBeginsAttack += disableActionsB;
        
        TurnBehaviour.OnPlayerAttack += enableActionsB; //this is when attack ends
        TurnBehaviour.OnActorFinishedMove += enableActionsB;
        TurnBehaviour.OnPlayerTurnStart += enableActionsB;
        TurnBehaviour.OnNewSelectedUnit += enableActionsB;

        //disables end turn when is enemy turn, attacking, moving, or using item
        TurnBehaviour.OnPlayerTurnEnd += disableEndTurn;
        TurnBehaviour.OnPlayerBeginsAttack += disableEndTurn;
        TurnBehaviour.OnPlayerStartMove += disableEndTurn;

        TurnBehaviour.OnPlayerTurnStart += enableEndturn;
        TurnBehaviour.OnPlayerAttack += enableEndturn;
        TurnBehaviour.OnPlayerJustMoved += enableEndturn;
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnPlayerStartMove -= disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd -= disableActionsB;
        TurnBehaviour.OnPlayerBeginsAttack -= disableActionsB;
        
        TurnBehaviour.OnPlayerAttack -= enableActionsB;
        TurnBehaviour.OnActorFinishedMove -= enableActionsB;
        TurnBehaviour.OnPlayerTurnStart -= enableActionsB;
        TurnBehaviour.OnNewSelectedUnit -= enableActionsB;

        TurnBehaviour.OnPlayerTurnEnd -= disableEndTurn;
        TurnBehaviour.OnPlayerBeginsAttack -= disableEndTurn;
        TurnBehaviour.OnPlayerStartMove -= disableEndTurn;

        TurnBehaviour.OnPlayerTurnStart -= enableEndturn;
        TurnBehaviour.OnPlayerAttack -= enableEndturn;
        TurnBehaviour.OnPlayerJustMoved -= enableEndturn;
    }

    /// <summary>
    /// Disables buttons while certain events are happens
    /// </summary>
    void disableActionsB()
    {
        if (cancel != null)
        {
            cancel.GetComponent<Button>().interactable = false;
            animator.SetTrigger("HideCancel");
        }
        if (move != null)
            move.GetComponent<Button>().interactable = false;
        if (skills != null)
            skills.GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// Tries to enable buttons if the unit has actions avilable
    /// </summary>
    void enableActionsB()
    {
        if (cancelButtonUp)//cancel button is up but player just moved/attacked/switched character. Do cancel button trigger
        {
            cancelButtonUp = false;
            animator.SetTrigger("AfterAction");
        }
        if (GameController.getSelected().getMoves() != 0)
        {
            if (cancel != null)
                cancel.GetComponent<Button>().interactable = true;
            if (move != null)
                move.GetComponent<Button>().interactable = true;
            if (skills != null)
                skills.GetComponent<Button>().interactable = true;
        }
    }

    void disableEndTurn()
    {
        end.GetComponent<Button>().interactable = false;
    }

    void enableEndturn()
    {
        end.GetComponent<Button>().interactable = true;
    }

    void OnClick0()
    {
        GameController.setUnit(0);
    }

    void OnClick1()
    {
        GameController.setUnit(1);
    }

    void OnClick2()
    {
        GameController.setUnit(2);
    }

    void OnClick3()
    {
        GameController.setUnit(3);
    }

    public bool CancelButtonUp
    {
        set { cancelButtonUp = value; }
    }
}
