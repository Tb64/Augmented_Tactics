using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{

    public int playerID;
    public static int playerNum;
    public static Actor[] playerList;
    //player controlled characters will control ui elements on screen
    protected HealthBar[] UIHealth;

    // Use this for initialization
    void Start ()
    {
        base.Start();

        //UIHealth = GameObject.Find("Player1Health").GetComponent<HealthBar>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("HealthBar");
        int i = 0;
        while (temp[i] != null)
        {
            UIHealth[i] = temp[i].GetComponent<HealthBar>();
            i++;
        }

        if (playerNum == null)
            playerNum = 0;
        if (playerList == null)
            playerList = new Actor[4];
        playerList[playerNum] = this;
        playerID = playerNum;
        Debug.Log("Player added: " + playerNum + ") " + playerList[playerNum]);
        playerNum++;

        abilitySet = new BasicAttack[4];
       // GameObject.FindWithTag("Map").GetComponent<TileMap>().Players.Add(this.GetComponent<Actor>());
    }

    void OnEnable()
    {
        numberOfActors++;
    }

    public virtual void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (UIHealth != null)
        {
            int i = 0;
            while (UIHealth[i] != null)
            {
                UIHealth[0].UpdateUIHealth(GetHealthPercent(), true);
                i++;
            }
                //UIHealth.UpdateUIHealth(GetHealthPercent(), true);
        }
    }



    // Update is called once per frame
    void Update () {
        base.Update();

        turnControl();
    }



    void turnControl()
    {

        //true player turn ,false enemy turn
        if (SM.GetComponent<StateMachine>().checkTurn() == true)
        {
            drawDebugLines();
            moveUnit();
        }

    }
}
