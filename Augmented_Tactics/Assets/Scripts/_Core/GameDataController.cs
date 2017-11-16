using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameDataController: MonoBehaviour
{
    private static string filePath;
    private GameData gameData;
    private void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "Saves\\data.json");
        gameData = new GameData();
    }
    public static List<PlayerData> loadPlayerData()
    {
        if (File.Exists(GameDataController.filePath))
        {
            string jsonData= File.ReadAllText(GameDataController.filePath);
            return JsonUtility.FromJson<List<PlayerData>>(jsonData);
        }
        else
        {
            Debug.LogError("Can't Find Game Data");
            return null;
        }
    }

    public static void savePlayerData(List<PlayerData> playerData)
    {
        Debug.Log(GameDataController.filePath);
        if (File.Exists(GameDataController.filePath))
        {
            string jsonData = JsonUtility.ToJson(playerData);
            File.WriteAllText(GameDataController.filePath, jsonData);
            return;
        }
        else
        {
            File.Create(GameDataController.filePath);
            string jsonData = JsonUtility.ToJson(playerData);
            File.WriteAllText(GameDataController.filePath, jsonData);
            return;
        }
    }

}
