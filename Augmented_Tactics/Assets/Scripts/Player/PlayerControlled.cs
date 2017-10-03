using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{

    public int playerID;
    public static int playerNum;
    public static Actor[] playerList;
    //player controlled characters will control ui elements on screen
    protected static HealthBar[] UIHealth;

    // Use this for initialization
    void Start ()
    {
        base.Start();
        
        if (playerNum == null)
            playerNum = 0;
        if (playerList == null)
            playerList = new Actor[4];
        //populate list of healthbars. This way each health bar is assigned to one player controlled character
        if (UIHealth == null)
        {
            UIHealth = new HealthBar[4];
            GameObject[] temp = GameObject.FindGameObjectsWithTag("HealthBar");
            for(int i=0; i<temp.Length; i++)
                if(temp[i]!=null)
                    UIHealth[i] = temp[i].GetComponent<HealthBar>();
        }
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
        Debug.Log(this.playerID);
        if (UIHealth != null)
            UIHealth[this.playerID].UpdateUIHealth(GetHealthPercent(), true);
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

    void assignHealthBars()
    {
        
    }
}
