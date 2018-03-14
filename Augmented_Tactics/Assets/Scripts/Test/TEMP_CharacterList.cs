using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_CharacterList : MonoBehaviour {

    public static TEMP_CharacterData[] dataArr;

	// Use this for initialization
	void Start () {
        Init();
    }

    public static void Init()
    {
        dataArr = new TEMP_CharacterData[4];

        dataArr[0].name = "Person1";
        dataArr[1].name = "Person2";
        dataArr[2].name = "Person3";
        dataArr[3].name = "Person4";

        dataArr[0].level = 1;
        dataArr[1].level = 1;
        dataArr[2].level = 1;
        dataArr[3].level = 1;

        dataArr[0].classType = "Thief";
        dataArr[1].classType = "Thief";
        dataArr[2].classType = "Thief";
        dataArr[3].classType = "Thief";

        dataArr[0].icon = "UI/Ability/warriorSkill3";
        dataArr[1].icon = "UI/Ability/warriorSkill3";
        dataArr[2].icon = "UI/Ability/warriorSkill3";
        dataArr[3].icon = "UI/Ability/warriorSkill3";

        dataArr[0].prefab = "PlayerClasses/ThiefModelTemp";
        dataArr[1].prefab = "PlayerClasses/ThiefModelTemp";
        dataArr[2].prefab = "PlayerClasses/ThiefModelTemp";
        dataArr[3].prefab = "PlayerClasses/ThiefModelTemp";
    }

    // Update is called once per frame
    void Update () {
		
	}
}
