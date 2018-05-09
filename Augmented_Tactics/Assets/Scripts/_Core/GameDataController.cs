using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataController: MonoBehaviour
{
    private static string filePath; //add android path in future
    public static GameData gameData;
    private void Start()
    {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
        //filePath = Application.dataPath + "/StreamingAssets\\Saves\\data.json";
        filePath = GetPath();
        gameData = loadPlayerData();
        if(gameData != null)
            ShardController.setShards(gameData.Shards);
    }

    private static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "saveData.json");
    }
    public static GameData loadPlayerData()
    {
        //gameData = new GameData();
        //if (gameData == null)
        //    Debug.Log("new data is null");
        //Static Functions do not call start
        filePath = GetPath();
        //Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string jsonData= File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(jsonData);
            if (gameData == null)
                gameData = new GameData();
            return gameData;
        }
        else
        {
            Debug.Log("Can't Find Game Data, making new data at " + filePath);
            gameData = new GameData();
            //this.gameData = gameData;
            return gameData;
        }
    }

    public static bool savePlayerData(GameData gameData)
    {
        //Static Functions do not call start
        filePath = GetPath();
        Debug.Log("saving to " + filePath);
        if (filePath == null)
        {
            Debug.LogError("Can't Find Game Data");
            return false;
        }
        if (File.Exists(GameDataController.filePath))
        {
            string jsonData = JsonUtility.ToJson(gameData);
            File.WriteAllText(GameDataController.filePath, jsonData);
            return true;
        }
        else
        {
            File.Create(GameDataController.filePath).Dispose();
            string jsonData = JsonUtility.ToJson(gameData);
            File.WriteAllText(GameDataController.filePath, jsonData);
            return true;
        }
    }

    public static bool savePlayerData()
    {
        //Static Functions do not call start
        filePath = GetPath();
        //Debug.Log(GameDataController.filePath);
        if (filePath == null)
        {
            Debug.LogError("Can't Find Game Data");
            return false;
        }
        if (File.Exists(filePath))
        {
            string jsonData = JsonUtility.ToJson(gameData);
            File.WriteAllText(filePath, jsonData);
            return true;
        }
        else
        {
            File.Create(filePath);
            string jsonData = JsonUtility.ToJson(gameData);
            File.WriteAllText(filePath, jsonData);
            return true;
        }
    }

}
