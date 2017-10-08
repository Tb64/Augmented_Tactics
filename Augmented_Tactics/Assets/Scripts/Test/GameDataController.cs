using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameDataController : MonoBehaviour {
    private string fileName = "data.json";
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameData loadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonData= File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
            return loadedData;
        }
        else
        {
            Debug.LogError("Can't Load Game Data");
            return null;
        }
    }

}
