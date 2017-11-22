using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataController: MonoBehaviour
{
    private static string filePath; //add android path in future
    private GameData gameData;
    private void Start()
    {
        filePath = Application.dataPath + "/StreamingAssets\\Saves\\data.json";
        gameData = loadPlayerData();
    }
    public static GameData loadPlayerData()
    {
        
        //Debug.Log(filePath);
        if (File.Exists(GameDataController.filePath))
        {
            string jsonData= File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            Debug.LogError("Can't Find Game Data");
            return null;
        }
    }

    public static bool savePlayerData(GameData gameData)
    {
        //Debug.Log(GameDataController.filePath);
        if(filePath == null)
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
            File.Create(GameDataController.filePath);
            string jsonData = JsonUtility.ToJson(gameData);
            File.WriteAllText(GameDataController.filePath, jsonData);
            return true;
        }
    }

}
