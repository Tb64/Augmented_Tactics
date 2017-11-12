using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIManager : MonoBehaviour {

    private static GameObject[] barFolders;
    private static Image[] playerHealthImg;
    private static Image[] playerManaImg;
    private static int numPlayers = 0;
    private static bool ranAlready = false;
        
    // this manager first gets all the health bars and disables them, enabling them 1 by one as player controlled units spawn
    // when a unit takes damage update all bars
    void Awake ()
    {
        TurnBehaviour.OnUnitSpawn += onUnitSpawn;
    }

	public static void updateHealth()
    {
        //would be harder to tell which enemy in particular took damage. Just update all bars
        for (int i = 0; i < numPlayers; i++)
            playerHealthImg[i].fillAmount = PlayerControlled.playerList[i].GetHealthPercent();
    }

    public void onUnitSpawn()
    {
        if(numPlayers < PlayerControlled.playerNum)
            if (!ranAlready)
            {
                init();
                ranAlready = true;
                if(barFolders!=null)
                    barFolders[numPlayers].SetActive(true);
                numPlayers++;
            }
            else
            {
                if (barFolders != null)
                    barFolders[numPlayers].SetActive(true);
                numPlayers++;
            }
    }

    public void OnDestroy()
    {
        TurnBehaviour.OnUnitSpawn -= onUnitSpawn;
    }

    private void getBars(int arraySlot)
    {
        Image[] bars = barFolders[arraySlot].GetComponentsInChildren<Image>();
        //Looks for all the images in the BarX game objects, finds the proper ones we will modify
        if (bars!=null)
            for (int i = 0; i < bars.Length; i++)
                if (bars[i].name == ("HFront" + (arraySlot + 1).ToString()))
                    playerHealthImg[arraySlot] = bars[i];
                else if (bars[i].name == ("MFront" + (arraySlot + 1).ToString()))
                    playerManaImg[arraySlot] = bars[i];
        barFolders[arraySlot].SetActive(false);
    }

    private void init()
    {
        GameObject[] tempObjs = GameObject.FindGameObjectsWithTag("UIHealthBar");

        //making sure everything is in order to activate the proper bars
        if (tempObjs != null)
        {
            barFolders = new GameObject[tempObjs.Length];
            playerHealthImg = new Image[tempObjs.Length];
            playerManaImg = new Image[tempObjs.Length];
            for (int i = 0; i < tempObjs.Length; i++)
            {
                switch (tempObjs[i].gameObject.name)
                {
                    case "Bar1":
                        barFolders[0] = tempObjs[i];
                        getBars(0);
                        break;
                    case "Bar2":
                        barFolders[1] = tempObjs[i];
                        getBars(1);
                        break;
                    case "Bar3":
                        barFolders[2] = tempObjs[i];
                        getBars(2);
                        break;
                    case "Bar4":
                        barFolders[3] = tempObjs[i];
                        getBars(3);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
