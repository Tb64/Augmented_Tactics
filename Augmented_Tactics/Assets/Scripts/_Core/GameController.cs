using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Image[] AbilityImages;
    public Text[] AbilityText;
    private RangeHighlight rangeMarker;

    private static Actor selectedUnit;
    private static GameObject targetUnit;
    private static ClickableTile clickedTile;
    private static TileMap map;
    private static Image[] abilityImages;
    private static Text[] abilityText;

    private int currentAbility = 0;
    private static bool abilityMode = false;
    // Use this for initialization
    void Start () {
        GameObject mapObj = GameObject.FindGameObjectWithTag("Map");
        if(mapObj != null)
        {
            map = mapObj.GetComponent<TileMap>();
        }
        GameObject rangeMarkerObj = GameObject.Find("RangeMarker");
        if (rangeMarkerObj != null)
            rangeMarker = rangeMarkerObj.GetComponent<RangeHighlight>();

        abilityImages = AbilityImages;
        abilityText = AbilityText;
    }
	
	// Update is called once per frame
	void Update () {
        ClickEvent();

    }

    private void ClickEvent()
    {
        if(Input.anyKey && abilityMode)
        {
            Debug.Log("Setting ability target");
            if(targetUnit != null)
                selectedUnit.abilitySet[currentAbility].UseSkill(targetUnit);
            rangeMarker.Marker_Off();
            abilityMode = false;
        }
    }

    //public override void TurnStart()
    //{
    //    base.TurnStart();
    //}

    //public override void EnemyTurnStart()
    //{
    //    base.EnemyTurnStart();
    //}

    //public override void PlayerTurnStart()
    //{
    //    base.PlayerTurnStart();
    //}

    public static void NewSelectedUnit()
    {
        if(abilityMode)
        {
            targetUnit = map.selectedUnit;
        }
        else
        {
            selectedUnit = map.selectedUnit.GetComponent<Actor>();
            SetAbilityButtons();
        }
    }

    public static void ClickedOnTile(ClickableTile input)
    {
        clickedTile = input;
    }

    public static void ClickedOnTarget(Actor input)
    {
        //targetUnit = input;
    }


    /****************
     *      UI      *
     ****************/

    public static void SetAbilityButtons()
    {
        for (int index = 0; index < abilityImages.Length; index++)
        {
            //Debug.Log("Updating Ability Bar");
            abilityImages[index].sprite = selectedUnit.abilitySet[index].abilityImage;
            abilityText[index].text = selectedUnit.abilitySet[index].abilityName;
        }
    }

    public void UseAbility(int abilityNum)
    {
        //rangeMarker.Marker_On();
        currentAbility = abilityNum;
        rangeMarker.Marker_On(selectedUnit.getMapPosition(), selectedUnit.abilitySet[currentAbility].range);
        abilityMode = true;
    }

    
}
