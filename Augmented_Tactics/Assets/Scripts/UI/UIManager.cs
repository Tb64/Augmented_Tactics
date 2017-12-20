using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameObject move;
    private GameObject skills;
    private GameObject end;
    
	void Start ()
    {
        //disable while moving, on enemy turn start, and if default unit is out of actions. Try to enable when switching
        //to new unit
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
                }
            }
        }

        TurnBehaviour.OnUnitBeginsMoving += disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd += disableActionsB;
        TurnBehaviour.OnUnitBeginsAttacking += disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd += disableEndTurn;

        TurnBehaviour.OnPlayerTurnStart += enableEndturn;
        TurnBehaviour.OnPlayerAttack += enableActionsB; //this is when attack ends
        TurnBehaviour.OnUnitMoved += enableActionsB;
        TurnBehaviour.OnPlayerTurnStart += enableActionsB;
        TurnBehaviour.OnNewSelectedUnit += enableActionsB;
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnUnitBeginsMoving -= disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd -= disableActionsB;
        TurnBehaviour.OnUnitBeginsAttacking -= disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd -= disableEndTurn;

        TurnBehaviour.OnPlayerTurnStart -= enableEndturn;
        TurnBehaviour.OnPlayerAttack -= enableActionsB;
        TurnBehaviour.OnUnitMoved -= enableActionsB;
        TurnBehaviour.OnPlayerTurnStart -= enableActionsB;
        TurnBehaviour.OnNewSelectedUnit += enableActionsB;
    }

    //check if a default unit has actions left when default unit changes
    //during movement disable
    //at the end of a turn disable movement, re-enable if available is >0

    void disableActionsB()
    {
        move.GetComponent<Button>().interactable = false;
        skills.GetComponent<Button>().interactable = false;
    }

    void enableActionsB()
    {
        if (GameController.getSelected().getMoves() != 0)
        {
            move.GetComponent<Button>().interactable = true;
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
}
