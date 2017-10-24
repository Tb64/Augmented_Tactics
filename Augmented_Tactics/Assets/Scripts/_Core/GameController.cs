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

    public GameObject selectedMarker;

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

    private int currentAbility = 0;
    private static bool abilityMode = false;
    private int currentMode = MODE_SELECT_UNIT;
    // Use this for initialization
    void Start()
    {
        TurnBehaviour.OnTurnStart += this.TurnStart;
        TurnBehaviour.OnPlayerTurnStart += this.PlayerTurnStart;
        GameObject mapObj = GameObject.FindGameObjectWithTag("Map");
        if (mapObj != null)
        {
            map = mapObj.GetComponent<TileMap>();
        }
        GameObject rangeMarkerObj = GameObject.Find("RangeMarker");
        if (rangeMarkerObj != null)
            rangeMarker = rangeMarkerObj.GetComponent<RangeHighlight>();

        if (PlayerControlled.playerList[0] != null)
        {
            selectedUnit = PlayerControlled.playerList[0];
            SetAbilityButtons();
        }

        abilityImages = AbilityImages;
        abilityText = AbilityText;
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.TurnStart;
        TurnBehaviour.OnPlayerTurnStart -= this.PlayerTurnStart;
    }

    // Update is called once per frame
    void Update()
    {
        ClickEvent();

    }

    private void TurnStart()
    {
        targetObject = null;
    }

    private void PlayerTurnStart()
    {

    }

    //private void ClickEvent()
    //{
    //    if(Input.anyKey && abilityMode)
    //    {
    //        Debug.Log("Setting ability target");
    //        if(targetUnit != null)
    //            selectedUnit.abilitySet[currentAbility].UseSkill(targetUnit);
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
            selectedMarker.transform.position = selectedUnit.transform.position;// + new Vector3(0f,2f,0f);
        }


    }

    void SelectTarget()
    {
        GameObject interactedObject = RayCaster();

        if (interactedObject == null || 
            !interactedObject.name.Contains("Tile") ||
            interactedObject.tag != "Enemy" ||
            interactedObject.tag != "Player")
            return;

        //if the same target is selected twice in a row do action

        if( targetObject == null || targetObject != interactedObject)
        {
            targetObject = interactedObject;
        }
        else if (targetObject == interactedObject)
        {
            selectedUnit.abilitySet[currentAbility].UseSkill(targetObject);
            currentMode = MODE_SELECT_UNIT;
        }


    }

    void SelectMoveLocation()
    {
        GameObject interactedObject = RayCaster();

        if (interactedObject != null && interactedObject.name.Contains("Tile"))
        {
            clickedTile = interactedObject.GetComponent<ClickableTile>();
            map.moveActor(selectedUnit.gameObject, clickedTile.getMapPosition());

            Debug.Log("Selected Tile: " + interactedObject.name + " pos " + clickedTile.getMapPosition());
        }

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

    public static void NewSelectedUnit()
    {
        if (abilityMode)
        {
            targetObject = map.selectedUnit;
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

    public void setMode(int mode)
    {
        currentMode = mode;
    }

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
        currentMode = MODE_SELECT_TARGET;
        rangeMarker.Marker_On(selectedUnit.getMapPosition(), selectedUnit.abilitySet[currentAbility].range);
        //abilityMode = true;
    }

    /************
     *  Get/Set
     ************/

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
