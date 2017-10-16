using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : TurnBehavoir {

    private Image[] AbilityImages;
    private Text[] AbilityText;
    private RangeHighlight rangeMarker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TurnStart()
    {
        base.TurnStart();
    }

    public override void EnemyTurnStart()
    {
        base.EnemyTurnStart();
    }

    public override void PlayerTurnStart()
    {
        base.PlayerTurnStart();
    }

    public static void NewSelectedUnit()
    {

    }


    /****************
     *      UI      *
     ****************/

    public void SetAbilityButtons()
    {

    }

    public void UseAbility(int abilityNum)
    {
        //rangeMarker.Marker_On();
        
    }

    
}
