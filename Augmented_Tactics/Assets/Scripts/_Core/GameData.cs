using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class GameData
{
    public List<PlayerData> armyList = new List<PlayerData>();
    public int Shards;
    public PlayerData[] currentTeam = 
   {
        new PlayerData("Player0"),
        new PlayerData("Player1"),
        new PlayerData("Player2"),
        new PlayerData("Player3")
    };

    public List<UsableItem> usableItems = new List<UsableItem>();
    public List<Armor> armors = new List<Armor>();
    public List<Weapons> weapons = new List<Weapons>();

    public bool loaded = false;
    public GameData()
    {

        Debug.Log("Generating New GameDat");
        armyList = new List<PlayerData>();
        //generateArmy();

        /*if (!loaded)
        {
          loaded = true;
          armyList = retrieveData();
         }
        Debug.Log(armyList[0]);
        List<PlayerData> tempPlayers = armyList;
        tempPlayers.Add(new PlayerData("Doogy"));
        tempPlayers.Add(new PlayerData("Testing"));
        tempPlayers.Add(new PlayerData("Saving"));
        tempPlayers.Add(new PlayerData("Loading"));
        armyList = tempPlayers;
        Debug.Log("added all players")*/
       // sendData();

    }

    public void generateArmy()
    {
        string temp = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "armyNames.txt"));
        StringReader stringReader = new StringReader(temp);
        string charName;
        charName = stringReader.ReadLine();
        while(charName != null)
        {
            Debug.Log(charName);
            armyList.Add(new PlayerData(charName));
            charName = stringReader.ReadLine();
        }
        /*for(int line = 0; line < 137; line++)
        {
            string tempName = charNames.
        }*/
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

    public static void replacePlayer(int teamPosition, PlayerData replacement)
    {
        PlayerPrefs.SetString("Player" + teamPosition, replacement.playerName);
    }
    public PlayerData[] getNextSix(int playerNum)
    {
        PlayerData[] next = new PlayerData[6];
        for (int x = 0; x < 6; x++)
        {
            if (playerNum >= armyList.Count || armyList[playerNum] == null)
                break;
            next[x] = armyList[playerNum];
            playerNum++;
        }

        return next;
    }
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

    public bool addPlayer(PlayerData newCharacter)
    {
        if(armyList != null)
        {
            armyList.Add(newCharacter);
        }
        return false;
    }

    /* private List<PlayerData> retrieveData()
     {
         //GameDataController game = new GameDataController();
         List<PlayerData> data = GameDataController.loadPlayerData();
         return data;
     }*/

    private void sendData()
    {
        // GameDataController game = new GameDataController();
        GameDataController.savePlayerData(this);
    }

}
