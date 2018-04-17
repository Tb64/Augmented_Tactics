using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public const int MODE_SELECT_UNIT       = 0;
    public const int MODE_SELECT_TARGET     = 1;
    public const int MODE_SELECT_LOCATION   = 2;
    public const int MODE_MOVE              = 3;
    public const int MODE_ACTION            = 4;

    public static GameObject selectedMarker;
    public static GameObject selectedUnitHighlight;

    private static Actor selectedUnit;
    private static Vector3 targetLocation;
    private static GameObject targetObject;
    private static ClickableTile clickedTile;
    private static TileMap map;
    private static Image[] abilityImages;
    private static Text[] abilityText;

    public Image[] AbilityImages;
    public Text[] AbilityText;
    private RangeHighlight rangeMarker;
    private RangeHighlight aoeMarker;
    private GameObject endOfBattleController;

    private static int currentAbility = 0;
    private static bool abilityMode = false;
    private static int currentMode = MODE_SELECT_UNIT;

    private void Awake()
    {
        Initialize();
    }

    // Use this for initialization
    void Initialize()
    {
        TurnBehaviour.OnTurnStart += this.TurnStart;
        TurnBehaviour.OnPlayerTurnStart += this.PlayerTurnStart;
        TurnBehaviour.OnPlayerSpawn += this.UnitSpawn;
        currentMode = MODE_SELECT_UNIT;
        GameObject mapObj = GameObject.FindGameObjectWithTag("Map");
        endOfBattleController = GameObject.Find("EndofBattleController");
        if (mapObj != null)
        {
            map = mapObj.GetComponent<TileMap>();
        }
        GameObject rangeMarkerObj = GameObject.Find("RangeMarker");
        if (rangeMarkerObj != null)
            rangeMarker = rangeMarkerObj.GetComponent<RangeHighlight>();

        GameObject aoeRangeMarkerObj = GameObject.Find("AttackMarker");
        if (aoeRangeMarkerObj != null)
            aoeMarker = aoeRangeMarkerObj.GetComponent<RangeHighlight>();

        if (PlayerControlled.playerList != null && PlayerControlled.playerList[0] != null)
        {
            selectedUnit = PlayerControlled.playerList[0];
            SetAbilityButtons();
        }
        selectedMarker = GameObject.Find("SelectMarker");
        selectedUnitHighlight = GameObject.Find("SelectUnitMarker");

        abilityImages = AbilityImages;
        abilityText = AbilityText;
    }

    private void UnitSpawn()
    {
        SelectDefaultUnit();
    }


    private void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.TurnStart;
        TurnBehaviour.OnPlayerTurnStart -= this.PlayerTurnStart;
        TurnBehaviour.OnPlayerSpawn -= this.UnitSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        ClickEvent();

    }

    private void TurnStart()
    {
        targetObject = null;
        BothMarkersOff();
      
        //endOfBattleController.GetComponent<AfterActionReport>().BattleOver();
    }

    private void PlayerTurnStart()
    {
        SelectDefaultUnit();
    }

    //private void ClickEvent()
    //{
    //    if(Input.anyKey && abilityMode)
    //    {
    //        Debug.Log("Setting ability target");
    //        if(targetUnit != null)
    //           selectedUnit.abilitySet[currentAbility].UseSkill(targetUnit);
    //        rangeMarker.Marker_Off();
    //        abilityMode = false;
    //    }
    //}

    void ClickEvent()
    {
        if (Input.GetMouseButtonDown(0) &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            switch (currentMode)
            {
                case MODE_SELECT_UNIT:
                    SelectUnit();
                    break;

                case MODE_SELECT_LOCATION:
                    //SelectLocation();
                    break;

                case MODE_SELECT_TARGET:
                    SelectTarget();
                    break;

                case MODE_MOVE:
                    SelectMoveLocation();
                    break;

                default:
                    break;
            }
        }
    }

    GameObject RayCaster()
    {
        GameObject interactedObject;
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            interactedObject = interactionInfo.collider.gameObject;
            Debug.Log("Click event on: " + interactedObject.name);
            if (selectedMarker != null)
                selectedMarker.transform.position = interactedObject.transform.position;// + new Vector3(0f,2f,0f);
            return interactedObject;
        }

        return null;
    }

    void SelectUnit()
    {
        GameObject interactedObject = RayCaster();
        if (interactedObject != null && interactedObject.tag == "Player")
        {
            Debug.Log("Selected Player: " + interactedObject.name);
            selectedUnit = interactedObject.GetComponent<Actor>();
            TurnBehaviour.NewSelectedUnit();
            //map.selectedUnit = interactedObject;
            SetAbilityButtons();
            if (selectedMarker != null)
                selectedMarker.transform.position = selectedUnit.transform.position;// + new Vector3(0f,2f,0f);
            if (selectedUnitHighlight != null)
                selectedUnitHighlight.GetComponent<SelectedUnitMarker>().AttachMarker(selectedUnit);
            else
                Debug.Log("UNIT MARKER NOT ATTACHED");
        }


    }

    void SelectTarget()
    {
        GameObject interactedObject = RayCaster();

        if (selectedUnit.isIncapacitated() == true)
        {
            Debug.Log("Unit is Incapacitated");
            return;     //prevents unit from selecting a target
        }

        if (interactedObject == null 
        //    || !interactedObject.name.Contains("Tile") 
        //    || interactedObject.tag != "Enemy" 
        //    || interactedObject.tag != "Player"
            )
            return;

        //if the same target is selected twice in a row do action

        if( targetObject == null || targetObject != interactedObject)
        {
            targetObject = interactedObject;
            if (aoeMarker != null)
            {
                aoeMarker.Marker_Off();
            }
            if (selectedUnit.abilitySet[currentAbility].isAOEAttack())
            {                
                selectedUnit.abilitySet[currentAbility].TargetSkill(targetObject);
            }
            TurnBehaviour.PlayerIsConfirmingAttack();
            Debug.Log("Initial Target selected, select again to confirm");
        }
        else if (targetObject == interactedObject)
        {
            bool attackedSuccess;

            attackedSuccess = selectedUnit.abilitySet[currentAbility].UseSkill(targetObject);
            if (attackedSuccess) { TurnBehaviour.ActorBeginsAttacking(); }
            BothMarkersOff();
            setMode(MODE_SELECT_UNIT);
            targetObject = null;
            //Debug.Log("Using ability " + selectedUnit.abilitySet[currentAbility].abilityName);
        }
    }

    void SelectMoveLocation()
    {
        GameObject interactedObject = RayCaster();
        
        if(selectedUnit.isIncapacitated() == true)
        {
            return;     //prevents unit from moving if incapacitated
        }

        if (interactedObject != null && interactedObject.name.Contains("Tile"))
        {
            clickedTile = interactedObject.GetComponent<ClickableTile>();

            Debug.Log("Selected Tile: " + interactedObject.name + " pos " + clickedTile.getCoords());
          
            map.moveActorAsync(selectedUnit.gameObject, clickedTile.getCoords());

            
            //selectedUnit.PlaySound("move");
        }

        setMode(MODE_SELECT_UNIT);
    }

    void SelectLocation()
    {
        GameObject interactedObject = RayCaster();
                
        if (interactedObject != null && interactedObject.name.Contains("Tile"))
        {
            Debug.Log("Selected Tile: " + interactedObject.name);
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

    public void SelectDefaultUnit()
    {
        if (selectedUnit == null && PlayerControlled.playerList != null && PlayerControlled.playerList[0] != null)
        {
            Debug.Log("Selecting Default");
            selectedUnit = PlayerControlled.playerList[0];
            //selectedUnit = PlayerControlled.playerObjs[0].GetComponent<Actor>();
            //GameObject gObj = PlayerControlled.playerList[0].gameObject;
            //selectedUnit = gObj.GetComponent<Actor>();
            if (selectedMarker != null)
                selectedMarker.transform.position = selectedUnit.transform.position;
            //if (selectedUnitHighlight != null)
            //    selectedUnitHighlight.GetComponent<SelectedUnitMarker>().AttachMarker(selectedUnit);


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
        if (abilityImages == null || selectedUnit.abilitySet == null)
            return;
        for (int index = 0; index < abilityImages.Length; index++)
        {
            //Debug.Log("Updating Ability Bar of " + selectedUnit.name);
            abilityImages[index].sprite = selectedUnit.abilitySet[index].abilityImage;
            abilityText[index].text = selectedUnit.abilitySet[index].abilityName;
        }
        //Debug.Log("Updating Ability Bar of " + selectedUnit.name);
    }

    public void UseAbility(int abilityNum)
    {
        if(selectedUnit.canAct() == false)
        {
            return;
        }

        //rangeMarker.Marker_On();
        if (aoeMarker != null) //turns off aoe marker when switching to another ability
        {
            aoeMarker.Marker_Off();
        }

        currentAbility = abilityNum;
        setMode(MODE_SELECT_TARGET);
        if (rangeMarker != null)
            rangeMarker.Attack_Marker_On(selectedUnit.getCoords(), selectedUnit.abilitySet[currentAbility].range_min, selectedUnit.abilitySet[currentAbility].range_max);
        //selectedUnit.abilitySet[currentAbility].range); broken after merge, commented out in meantime -Arthur
        //abilityMode = true;

    }

    /************
     *  Get/Set
     ************/

    public void setMode(int mode)
    {
        currentMode = mode;
        Debug.Log("Mode Changed to " + mode);
    }

    public static int getMode()
    {
        return currentMode;
    }

    public static int getCurrentAbility()
    {
        return currentAbility;
    }

    public void setMove()
    {
        currentMode = MODE_MOVE;
        BothMarkersOff();
        if(rangeMarker != null)
            rangeMarker.Move_Marker_On(selectedUnit.getCoords(), selectedUnit.moveDistance); 
    }

    public static Actor getSelected()
    {
        return selectedUnit;
    }

    public void setAOEMarker(Vector3 markerCoor)
    {
        if (aoeMarker != null)
        {
            aoeMarker.AOE_Marker_On(markerCoor);
        }

    }

    public static GameObject getTargeted()
    {
        return targetObject;
    }

    /// <summary>
    /// Turns off both aoeMarker and rangeMarker
    /// </summary>
    public void BothMarkersOff()
    {
        if (rangeMarker != null)
        {
            rangeMarker.Marker_Off();
        }
        if (aoeMarker != null)
        {
            aoeMarker.Marker_Off();
        }
    }

    /// <summary>
    /// Takes in the number of which player you want to make the selected unit
    /// </summary>
    public static void setUnit(int playerNum)
    {
        if (PlayerControlled.playerList[playerNum] != null)
        {
            Debug.Log("Selected Player: " + PlayerControlled.playerList[playerNum].name);
            selectedUnit = PlayerControlled.playerList[playerNum].GetComponent<Actor>();
            TurnBehaviour.NewSelectedUnit();
            //map.selectedUnit = interactedObject;
            SetAbilityButtons();
            if (selectedUnitHighlight != null)
                selectedUnitHighlight.GetComponent<SelectedUnitMarker>().AttachMarker(selectedUnit);
            else
                Debug.Log("UNIT MARKER NOT ATTACHED");
        }
    }

    Vector3 GetSelectedLocation(GameObject input)
    {
        Vector3 output = new Vector3(-1,-1,-1);
        if (input.tag == "Player" || input.tag == "Enemy")
            output = input.GetComponent<Actor>().getCoords();

        if (input.tag == "Tile" || input.name.Contains("Tile"))
            output = input.GetComponent<Actor>().getCoords();

        return output;
    }
}
