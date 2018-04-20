using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AfterActionReport : MonoBehaviour {

    public GameObject[] playerFrame; 
    public GameObject[] playerImage;
    public GameObject lootObj;
    GameObject camera;
    GameObject screen;
    SceneManagement manager;
    Transform cameraPosition;

    private void Start()
    {
        manager = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        if (manager == null)
            Debug.Log("Scene Manager Not Found !!!!");

        GameObject screen = transform.Find("EndofBattleScreen").gameObject;
        if (screen == null)
            Debug.Log("End of Battle Screen Not Found !!!!");

        camera = GameObject.Find("CamFocus");
        // cameraPosition = GameObject.Find("cameraLocation").transform;

        DisplayExp();
    }

    public bool win()
    {
        int win = 0;
        for(int index = 0; index < EnemyController.enemyNum; index++)
        {
            if(EnemyController.enemyList[index].isIncapacitated() == true || EnemyController.enemyList[index].isDead() == true)
            {
                win++;
            }
        }

        if(win == EnemyController.enemyNum)
        {
            return true;
        }
        return false;
    }

    public bool lose()
    {
        int lose = 0;
        for (int index = 0; index < PlayerControlled.playerNum; index++)
        {
            if (PlayerControlled.playerList[index].isIncapacitated() == true || PlayerControlled.playerList[index].isDead() == true)
            {
                lose++;
            }
        }

        if (lose == PlayerControlled.playerNum)
        {
            return true;
        }
        return false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        //manager.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        manager.LoadHub();
    }

    public void DisplayExp()
    {
        //load data
        GameDataController.loadPlayerData();
        TEMP_CharacterList.Init();
        PlayerData data = TEMP_CharacterList.characterData[0];

        if (data == null)
            Debug.Log("Data failed to gerenate");

        if (GameDataController.gameData == null)
            Debug.Log("Data failed to make Game Date");
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[0]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[1]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[2]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[3]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[0]);

            

        //get the amount of XP gained from enemies and divide them by numer of players
        int expTotal = 0;

        for (int index = 0; index < EnemyController.enemyNum; index++)
        {
            expTotal += EnemyController.enemyList[index].getExpGiven();
        }

        expTotal = (int)expTotal / PlayerControlled.playerNum;

        for (int index = 0; index < PlayerControlled.playerNum; index++)
        {
            PlayerControlled.playerList[index].setExperience(expTotal);
        }

        // Each first number in the array will correspond to a playerFrame (1-4)
        Text[][] textElements = new Text[4][];

        //get all text elements in the frame
        for (int i = 0; i < 4; i++)
        {
            if (playerFrame[i] != null)
                textElements[i] = playerFrame[i].GetComponentsInChildren<Text>();
        }
        
        //update all frames. Loop 1 is for going through each player frame, loop 2 is for going through all text elements
        for (int frame = 0; frame < 4; frame++)
        {
            //if on a frame that is more than the number of players (e.g. 3rd frame but 2 players), then disable game object
            //else set values
            if (frame > PlayerControlled.playerNum)
                playerFrame[frame].SetActive(false);
            else
            {
                //set slider percentage. Experience/Experience to next valu
                playerFrame[frame].GetComponentInChildren<Slider>().value = .5f;

                //set the image
                //playerImage[frame].GetComponent<Image>().sprite = 

                //set all text elements
                foreach (Text textObj in textElements[frame])
                {
                    if (textObj.name == "Level")
                        textObj.text = "Level here";
                    else if (textObj.name == "Name")
                        textObj.text = "Name here"; //expTotal.ToString();
                    else if (textObj.name == "Experience")
                        textObj.text = "Experience/here";
                }
            }
        }
        //set the money collected and load images for everything colleted
        lootObj.GetComponentInChildren<Text>();
    }

    void movePlayers()
    {
        camera.transform.position = cameraPosition.position;
        //PlayerControlled.playerObjs[0].transform


    }


    public void BattleOver()
    {
        GameDataController.savePlayerData(GameDataController.gameData);

        if (win() == true || lose() == true && screen.GetComponent<Canvas>().enabled == false)
        {
            screen.GetComponent<Canvas>().enabled = true;
            DisplayExp();
            Time.timeScale = 0;

            movePlayers();

        }
    }
}
