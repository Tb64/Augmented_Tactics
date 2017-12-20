using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject move;
    public GameObject skills; //important to find later so people cant change these and break the game
    
	void Start ()
    {
        //disable while moving, on enemy turn start, and if default unit is out of actions. Try to enable when switching
        //to new unit
        TurnBehaviour.OnUnitBeginsMoving += disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd += disableActionsB;
        TurnBehaviour.OnUnitMoved += enableActionsB;
        TurnBehaviour.OnPlayerTurnStart += enableActionsB;
        TurnBehaviour.OnNewSelectedUnit += enableActionsB;
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnUnitBeginsMoving -= disableActionsB;
        TurnBehaviour.OnPlayerTurnEnd -= disableActionsB;
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
}
