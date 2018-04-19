using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanGenTest : MonoBehaviour {
    public PlayerData player;
    public List<PlayerData> army;
	// Use this for initialization
	void Start () {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;

        player = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        GameData gData = GameDataController.loadPlayerData();
        army = gData.armyList;
        GameDataController.gameData.currentTeam[0] = army[0];
        //if (GameDataController.gameData == null)
        //    Debug.Log("null gamedata");
        //else
        //    GameDataController.gameData.addPlayer(player);

        GameDataController.savePlayerData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
