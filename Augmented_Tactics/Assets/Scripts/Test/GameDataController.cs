using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameDataController {
    //private string fileName = "data.json";
    string filePath = Path.Combine(Application.streamingAssetsPath, "Saves\\data.json");
    public List<PlayerData> loadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string jsonData= File.ReadAllText(filePath);
            return JsonUtility.FromJson<List<PlayerData>>(jsonData);
            //return loadedData;
        }
        else
        {
            Debug.LogError("Can't Find Game Data");
            return null;
        }
    }

    public void savePlayerData(List<PlayerData> playerData)
    {
        if (File.Exists(filePath))
        {
            string jsonData = JsonUtility.ToJson(playerData);
            File.WriteAllText(filePath, jsonData);
            return;
        }
        else
        {
            File.Create(filePath);
            string jsonData = JsonUtility.ToJson(playerData);
            File.WriteAllText(filePath, jsonData);
            return;
        }
    }

}
