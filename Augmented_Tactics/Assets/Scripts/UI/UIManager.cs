using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject move;
    public GameObject skills;
    
	void Start ()
    {
        //disable while moving, on enemy turn start, if default unit is out of actions
        TurnBehaviour.OnUnitBeginsMoving += disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd += disableActionsB;
        TurnBehaviour.OnUnitMoved += enableActionsB;
        TurnBehaviour.OnPlayerTurnStart += enableActionsB;
        //enable at player turn start, when unit is done moving, if default unit has >1 actions
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnUnitBeginsMoving -= disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd -= disableActionsB;
        TurnBehaviour.OnUnitMoved -= enableActionsB;
        TurnBehaviour.OnPlayerTurnStart -= enableActionsB;
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
}
