using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData{
    public  List<PlayerData> armyList = new List<PlayerData>();
    private KeyValuePair<string,string>[] currentTeam = { new KeyValuePair<string, string>("Player0", ""),
    new KeyValuePair<string, string>("Player1", ""), new KeyValuePair<string, string>("Player2", ""),
    new KeyValuePair<string, string>("Player3", "")};
    public bool loaded = false;
    public GameData()
    {
        //if (!loaded)
        //{
           // loaded = true;
         //   armyList = retrieveData();
       // }
        //Debug.Log(armyList[0]);
        /*List<PlayerData> tempPlayers = armyList;
        tempPlayers.Add(new PlayerData("Doogy"));
        tempPlayers.Add(new PlayerData("Testing"));
        tempPlayers.Add(new PlayerData("Saving"));
        tempPlayers.Add(new PlayerData("Loading"));
        armyList = tempPlayers;*/
        //Debug.Log("added all players")
       // sendData();

    }
    #region set/gets
    public PlayerData[] getCurrentTeam() 
    {
        PlayerData[] tempTeam = new PlayerData[4];
        tempTeam[0] = loadPlayer(PlayerPrefs.GetString("Player0"));
        tempTeam[1] = loadPlayer(PlayerPrefs.GetString("Player1"));
        tempTeam[2] = loadPlayer(PlayerPrefs.GetString("Player2"));
        tempTeam[3] = loadPlayer(PlayerPrefs.GetString("Player3"));
        return tempTeam;
        //no null checking. Must be checked when adding players to game
    }
    public void setCurrentTeam(string[] teamNames)
    {
        PlayerPrefs.SetString("Player0", teamNames[0]);
        PlayerPrefs.SetString("Player1", teamNames[1]);
        PlayerPrefs.SetString("Player2", teamNames[2]);
        PlayerPrefs.SetString("Player3", teamNames[3]);
    }
    public List<PlayerData> getArmyList()
    {
        return armyList;
    }
    public void setArmyList(List<PlayerData> army)
    {
        armyList = army;
    }
    #endregion

    public PlayerData loadPlayer(string name)
    {
        if (name == null)
        {
            return null;
        }
        else
        {
            foreach (PlayerData player in armyList)
            {
                if (player.getPlayerName() == name)
                {
                    return player;
                }
            }
            return null;
        }
    }

    public bool savePlayer(PlayerData newStats)
    {
        int playerIndex = 0;
        foreach (PlayerData stats in armyList)
        {
            if (stats.getPlayerName().Equals(newStats.getPlayerName()))
            {
                armyList[playerIndex] = newStats;
                sendData();
                return true;
            }
            playerIndex++;
        }

        //Debug.LogError("Unable to save");
        
        return false;
    }

   /* private List<PlayerData> retrieveData()
    {
        //GameDataController game = new GameDataController();
        List<PlayerData> data = GameDataController.loadPlayerData();
        return data;
    }*/

    private  void sendData()
    {
       // GameDataController game = new GameDataController();
        GameDataController.savePlayerData(this);
    }

}
