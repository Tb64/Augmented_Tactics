using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanGenTest : MonoBehaviour {
    public PlayerData player;
    public List<PlayerData> army;
    public GameData gData;
	// Use this for initialization
	void Start () {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
        Armor armor = ArmorGen.ArmorGenerate(1,CharacterClasses.BrawlerString, 4);
        Weapons weapon = WeaponGen.WeaponGenerate(1, CharacterClasses.BrawlerString, 0);
        //player = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        gData = GameDataController.loadPlayerData();
        //GameDataController.gameData.addPlayer(player);
        GameDataController.gameData.armors.Add(armor);
        //GameDataController.gameData.weapons.Add(weapon);
        //army = gData.armyList;
        //GameDataController.gameData.currentTeam[0] = army[0];
        //if (GameDataController.gameData == null)
        //    Debug.Log("null gamedata");
        //else
        //GameDataController.gameData.addPlayer(player);

        //GameDataController.savePlayerData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
