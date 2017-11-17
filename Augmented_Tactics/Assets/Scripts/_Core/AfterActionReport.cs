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
        manager.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        manager.LoadHub();
    }

    public void DisplayExp()
    {
        Text Exp1 = GameObject.Find("Exp1").gameObject.GetComponent<Text>();
        Text Exp2 = GameObject.Find("Exp2").gameObject.GetComponent<Text>();
        Text Exp3 = GameObject.Find("Exp3").gameObject.GetComponent<Text>();
        Text Exp4 = GameObject.Find("Exp4").gameObject.GetComponent<Text>();

        int expTotal = 0;

        for (int index = 0; index < EnemyController.enemyNum; index++)
        {
            expTotal += EnemyController.enemyList[index].getExpGiven();
        }

        expTotal = (int)expTotal / 4;

        for (int index = 0; index < PlayerControlled.playerNum; index++)
        {
            PlayerControlled.playerList[index].setExperience(expTotal);
        }

        Exp1.text = expTotal.ToString();
        Exp2.text = expTotal.ToString();
        Exp3.text = expTotal.ToString();
        Exp4.text = expTotal.ToString();

    }

    public void BattleOver()
    {
        if(win() == true || lose() == true)
        {
            GameObject screen = transform.Find("EndofBattleScreen").gameObject;
            screen.GetComponent<Canvas>().enabled = true;
            DisplayExp();
        }
    }
}
