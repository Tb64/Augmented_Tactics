using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentMoneyUI : MonoBehaviour {

    public Text moneyText;

	// Use this for initialization
	void Start () {
        GameDataController.loadPlayerData();
        moneyText.text = "" + GameDataController.gameData.Shards;
		
	}
	
    public void UpdateMoneyUI()
    {
        moneyText.text = "" + GameDataController.gameData.Shards;
    }
}
