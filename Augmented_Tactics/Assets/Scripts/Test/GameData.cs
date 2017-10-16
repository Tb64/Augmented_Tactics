using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData{
    private static List<PlayerData> allPlayers;
    private static bool loaded = false;
    public GameData()
    {
        if (!loaded)
        {
            loaded = true;
            allPlayers = retrieveData();
        }
    }
     
    /*public List<PlayerData> getAllPlayers()
    {
        return allPlayers;
    }*/
    public PlayerData findPlayer(string name)
    {
        foreach (PlayerData player in allPlayers)
        {
            if(player.getPlayerName() == name)
            {
                return player;
            }
        }
        Debug.LogError("Player " + name + " does not exist!");
        return null;
    }

    public bool savePlayer(PlayerData newStats)
    {
        int playerIndex = 0;
        foreach (PlayerData stats in allPlayers)
        {
            if (stats.getPlayerName().Equals(newStats.getPlayerName()))
            {
                allPlayers[playerIndex] = newStats;
                sendData();
                return true;
            }
            playerIndex++;
        }
        Debug.LogError("Unable to save");
        return false;
    }
    private static List<PlayerData> retrieveData()
    {
        GameDataController game = new GameDataController();
        List<PlayerData> data = game.loadPlayerData();
        return data;
    }

    private static void sendData()
    {
        GameDataController game = new GameDataController();
        game.savePlayerData(allPlayers);
    }

}
