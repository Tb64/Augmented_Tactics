using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AfterActionReport : MonoBehaviour {

    SceneManagement manager;

    private void Start()
    {
        if (GameObject.Find("SceneManager") != null)
        {
            manager = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        }
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
        manager.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        manager.LoadHub();
    }

    public void DisplayExp()
    {
       

        Text Exp1 = null;
        Text Exp2 = null;
        Text Exp3 = null;
        Text Exp4 = null;

        if (GameObject.Find("Exp1") != null)
        {
            Exp1 = GameObject.Find("Exp1").gameObject.GetComponent<Text>();
        }
        if(GameObject.Find("Exp2") != null)
        {
            Exp2 = GameObject.Find("Exp2").gameObject.GetComponent<Text>();
        }
        if(GameObject.Find("Exp3") != null)
        {
            Exp3 = GameObject.Find("Exp3").gameObject.GetComponent<Text>();
        }
        if (GameObject.Find("Exp4") != null)
        {
            Exp4 = GameObject.Find("Exp4").gameObject.GetComponent<Text>();
        }

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

        GameObject[] playerArray = new GameObject[4 ];

        if (GameObject.Find("Exp1") != null)
        {
            playerArray[0] = Exp1.transform.parent.gameObject;
            Exp1.text = expTotal.ToString();
        }
        if (GameObject.Find("Exp2") != null)
        {
            playerArray[1] = Exp2.transform.parent.gameObject;
            Exp2.text = expTotal.ToString();
        }
        if (GameObject.Find("Exp3") != null)
        {
            playerArray[2] = Exp3.transform.parent.gameObject;
            Exp3.text = expTotal.ToString();
        }
        if (GameObject.Find("Exp4") != null)
        {
            playerArray[3] = Exp4.transform.parent.gameObject;
            Exp4.text = expTotal.ToString();
        }

        

        for (int index = 3; index > PlayerControlled.playerNum - 1; index--)
        {
            playerArray[index].gameObject.SetActive(false);
            Debug.Log("index: " + index);
        }

        

    }

    public void BattleOver()
    {
        GameObject screen = transform.Find("EndofBattleScreen").gameObject; ;
        GameDataController.savePlayerData(GameDataController.gameData);

        if (win() == true || lose() == true && screen.GetComponent<Canvas>().enabled == false)
        {
            screen.GetComponent<Canvas>().enabled = true;
            DisplayExp();
            Time.timeScale = 0;

        }
    }
}
