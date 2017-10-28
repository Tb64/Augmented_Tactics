using System.Collections;
using System.Collections.Generic;
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
        for(int index = 0; index < Enemy.enemyNum; index++)
        {
            if(Enemy.enemyList[index].isIncapacitated() == true || Enemy.enemyList[index].isDead() == true)
            {
                win++;
            }
        }

        if(win == Enemy.enemyNum)
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

    public void battleOver()
    {
        if(win() == true || lose() == true)
        {
            manager.loadHub();
        }
    }
}
