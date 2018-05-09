using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class will handle all the updating of the healthbars and action markers for each unit, dimming them and activating them as necessary
public class HealthBarUIManager : MonoBehaviour {

    private static GameObject[] healthBarObj;
    private static Image[] playerHealthImg;
    private static Image[] playerManaImg;
    private static Image[] healthCircleImg;
    //One array for storing every player's actions. [numPlyaers][ActionImg]
    private static Image[,] playerActionsMarker;
    private static int numPlayers = 0;
    private static bool ranAlready = false;
    private const int MAXACTIONS = 2;
    private static int availableActions;

    private static Color32 dimCircleColor = new Color32(184, 150, 91, 255);
    private static Color32 normalColor = new Color32(255,255,255,255);
    private static Color32 dimActionMarkerColor = new Color32(190, 100, 100, 255);

    void Awake ()
    {
        TurnBehaviour.OnPlayerSpawn += onUnitSpawn;
        TurnBehaviour.OnActorFinishedMove += updateActions;
        TurnBehaviour.OnActorAttacked += updateActions;
        TurnBehaviour.OnPlayerTurnStart += onPlayerTurnStart;
        TurnBehaviour.OnActorAttacked += UpdateAll;
        TurnBehaviour.OnNewSelectedUnit += OnNewSelectedUnit;
        //TurnBehaviour.OnUnitDestroy
    }

    void OnDestroy()
    {
        TurnBehaviour.OnPlayerSpawn -= onUnitSpawn;
        TurnBehaviour.OnActorFinishedMove -= updateActions;
        TurnBehaviour.OnActorAttacked -= updateActions;
        TurnBehaviour.OnPlayerTurnStart -= onPlayerTurnStart;
        TurnBehaviour.OnActorAttacked -= UpdateAll;
        TurnBehaviour.OnNewSelectedUnit -= OnNewSelectedUnit;
        onPlayerTurnStart();        
        numPlayers = 0;
    }
    
    //used to dim the other bars on the none seletced units
    private void OnNewSelectedUnit()
    {
        //when a bar is clicked it gets stays the same color, other bars change to a darker color        
        for (int i = 0; i < numPlayers; i++)
        {
            //if a player is the one selected set it normal. Else dim the corresponding numbered healthbars
            if (GameController.getSelected() == PlayerControlled.playerList[i])
                healthCircleImg[i].color = normalColor;
            else
                healthCircleImg[i].color = dimCircleColor;
        }
    }

	public static void updateHealth()
    {
        //would be harder to tell which enemy in particular took damage. Just update all bars
        for (int i = 0; i < numPlayers; i++)
        {
            playerHealthImg[i].fillAmount = PlayerControlled.playerList[i].GetHealthPercent();
        }

        //TODO tint entire bar red if the unit has been destroyed
        //in actor thwre is a var called dead its true. .isDead()
    }

    public static void updateMana()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if (PlayerControlled.playerList[i].getMaxMana() == 0 || PlayerControlled.playerList[i].getManaCurrent() == 0)
                playerManaImg[i].fillAmount = 0f;
            else
                playerManaImg[i].fillAmount = PlayerControlled.playerList[i].GetManaPercent();
        }
    }

    //updates mana after a player attacks, loads a status effect image if one is active
    public static void UpdateAll()
    {
        updateHealth();
        updateMana();
        //check for a status effect. If one exists instantiate a status effect game object
        //Load the status effect prefab, then instantiate it. Set its transform to the appropriate health bar, finally add some force to make it spin
        /*GameObject statEffectObj = Instantiate<GameObject>(Resources.Load<GameObject>("UI/StatusEffect"));
        statEffectObj.transform.SetParent(healthBarObj[i].transform, false);
        statEffectObj.GetComponent<Rigidbody2D>().AddTorque(5);*/
        // spawn status effect sprue
    }

    //refresh movements
    private static void onPlayerTurnStart()
    {
        for (int i = 0; i < numPlayers; i++)
            for (int j = 0; j < MAXACTIONS; j++)
                playerActionsMarker[i, j].color = normalColor;
    }

    //dim action markers when and action is consumed
    private static void updateActions()
    {
        // if less than max actions then disable a marker
        for (int i = 0; i < numPlayers; i++)
        {
            availableActions = PlayerControlled.playerList[i].getMoves();
            if(availableActions < MAXACTIONS)
                playerActionsMarker[i, availableActions].color = dimActionMarkerColor;
        }
    }

    //activate a healthbar for each friendly that spawns at the beggining of the game.
    //On the first unit that spawns it activates init()
    private void onUnitSpawn()
    {
        //if it was a play controlled that was just added, not an enemy.
        if(numPlayers < PlayerControlled.playerNum)
            if (!ranAlready)
            {
                init();
                ranAlready = true;
                if (healthBarObj != null)
                {
                    healthBarObj[numPlayers].SetActive(true);
                    //healthCircleImg = PlayerControlled.playerList[numPlayers]
                }
                numPlayers++;
            }
            else
            {
                if (healthBarObj != null)
                {
                    healthBarObj[numPlayers].SetActive(true);
                    healthCircleImg[numPlayers].color = dimCircleColor; //dim the bar of the unit not selected
                }
                numPlayers++;
            }
        UpdateAll();
    }

    //organizes bars to into correct arrays and variables
    private void init()
    {
        GameObject[] tempObjs = GameObject.FindGameObjectsWithTag("UIHealthBar");

        //making sure everything is in order to activate the proper bars
        if (tempObjs != null)
        {            
            playerActionsMarker = new Image[tempObjs.Length, MAXACTIONS];
            healthBarObj = new GameObject[tempObjs.Length];
            playerHealthImg = new Image[tempObjs.Length];
            playerManaImg = new Image[tempObjs.Length];
            healthCircleImg = new Image[tempObjs.Length];
            for (int i = 0; i < tempObjs.Length; i++)
            {
                switch (tempObjs[i].gameObject.name)
                {
                    case "Bar1":
                        healthBarObj[0] = tempObjs[i];
                        getBars(0);
                        break;
                    case "Bar2":
                        healthBarObj[1] = tempObjs[i];
                        getBars(1);
                        break;
                    case "Bar3":
                        healthBarObj[2] = tempObjs[i];
                        getBars(2);
                        break;
                    case "Bar4":
                        healthBarObj[3] = tempObjs[i];
                        getBars(3);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //assistant method to init
    private void getBars(int arraySlot)
    {
        Image[] bars = healthBarObj[arraySlot].GetComponentsInChildren<Image>();
        //Looks for all the images in the BarX game objects
        if (bars != null)
            for (int i = 0; i < bars.Length; i++)
                if (bars[i].name == ("HFront" + (arraySlot + 1).ToString()))
                    playerHealthImg[arraySlot] = bars[i];
                else if (bars[i].name == ("MFront" + (arraySlot + 1).ToString()))
                    playerManaImg[arraySlot] = bars[i];
                else if (bars[i].name == ("ActionMA" + (arraySlot + 1).ToString()))
                    playerActionsMarker[arraySlot, 0] = bars[i];
                else if (bars[i].name == ("ActionMB" + (arraySlot + 1).ToString()))
                    playerActionsMarker[arraySlot, 1] = bars[i];
                else if (bars[i].name == ("HC" + (arraySlot + 1).ToString()))
                    healthCircleImg[arraySlot] = bars[i];
        healthBarObj[arraySlot].SetActive(false);
    }
}
