using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_CharacterList : MonoBehaviour {

    public static PlayerData[] characterData;

	// Use this for initialization
	void Start () {
        Init();
    }

    public static void Init()
    {
        int index;
        characterData = new PlayerData[4];

        index = 0;
        characterData[index] = new PlayerData("TEST1");
        characterData[index].setStatbyKey(PlayerKey.DisplayName, "TEST1");
        characterData[index].setStatbyKey(PlayerKey.Health, 100);
        characterData[index].setStatbyKey(PlayerKey.Mana, 20);
        characterData[index].setStatbyKey(PlayerKey.Level, 1);
        characterData[index].setStatbyKey(PlayerKey.Strength, 5);
        characterData[index].setStatbyKey(PlayerKey.Dexterity, 5);
        characterData[index].setStatbyKey(PlayerKey.Constitution, 5);
        characterData[index].setStatbyKey(PlayerKey.Intelligence, 5);
        characterData[index].setStatbyKey(PlayerKey.Wisdom, 5);
        characterData[index].setStatbyKey(PlayerKey.ClassName, "Knight");
        characterData[index].setStatbyKey(PlayerKey.Icon, "UI/Ability/warrior/warriorSkill6");
        characterData[index].setStatbyKey(PlayerKey.Prefab, "PlayerClasses/Thief");
        characterData[index].setStatbyKey(PlayerKey.Skill1, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill2, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill3, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill4, "basicattack");

        index = 1;
        characterData[index] = new PlayerData("TEST2");
        characterData[index].setStatbyKey(PlayerKey.DisplayName, "TEST2");
        characterData[index].setStatbyKey(PlayerKey.Health, 100);
        characterData[index].setStatbyKey(PlayerKey.Mana, 20);
        characterData[index].setStatbyKey(PlayerKey.Level, 1);
        characterData[index].setStatbyKey(PlayerKey.Strength, 5);
        characterData[index].setStatbyKey(PlayerKey.Dexterity, 5);
        characterData[index].setStatbyKey(PlayerKey.Constitution, 5);
        characterData[index].setStatbyKey(PlayerKey.Intelligence, 5);
        characterData[index].setStatbyKey(PlayerKey.Wisdom, 5);
        characterData[index].setStatbyKey(PlayerKey.ClassName, "Brawler");
        characterData[index].setStatbyKey(PlayerKey.Icon, "UI/Ability/warrior/warriorSkill5");
        characterData[index].setStatbyKey(PlayerKey.Prefab, "PlayerClasses/Brawler");
        characterData[index].setStatbyKey(PlayerKey.Skill1, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill2, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill3, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill4, "basicattack");

        index = 2;
        characterData[index] = new PlayerData("TEST3");
        characterData[index].setStatbyKey(PlayerKey.DisplayName, "TEST3");
        characterData[index].setStatbyKey(PlayerKey.Health, 100);
        characterData[index].setStatbyKey(PlayerKey.Mana, 20);
        characterData[index].setStatbyKey(PlayerKey.Level, 1);
        characterData[index].setStatbyKey(PlayerKey.Strength, 5);
        characterData[index].setStatbyKey(PlayerKey.Dexterity, 5);
        characterData[index].setStatbyKey(PlayerKey.Constitution, 5);
        characterData[index].setStatbyKey(PlayerKey.Intelligence, 5);
        characterData[index].setStatbyKey(PlayerKey.Wisdom, 5);
        characterData[index].setStatbyKey(PlayerKey.ClassName, "Cleric");
        characterData[index].setStatbyKey(PlayerKey.Icon, "UI/Ability/priest/priestSkill7");
        characterData[index].setStatbyKey(PlayerKey.Prefab, "PlayerClasses/Cleric");
        characterData[index].setStatbyKey(PlayerKey.Skill1, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill2, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill3, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill4, "basicattack");

        index = 3;
        characterData[index] = new PlayerData("TEST4");
        characterData[index].setStatbyKey(PlayerKey.DisplayName, "TEST4");
        characterData[index].setStatbyKey(PlayerKey.Health, 100);
        characterData[index].setStatbyKey(PlayerKey.Mana, 20);
        characterData[index].setStatbyKey(PlayerKey.Level, 1);
        characterData[index].setStatbyKey(PlayerKey.Strength, 5);
        characterData[index].setStatbyKey(PlayerKey.Dexterity, 5);
        characterData[index].setStatbyKey(PlayerKey.Constitution, 5);
        characterData[index].setStatbyKey(PlayerKey.Intelligence, 5);
        characterData[index].setStatbyKey(PlayerKey.Wisdom, 5);
        characterData[index].setStatbyKey(PlayerKey.ClassName, "Thief");
        characterData[index].setStatbyKey(PlayerKey.Icon, "UI/Ability/assassin/assassinSkill4");
        characterData[index].setStatbyKey(PlayerKey.Prefab, "PlayerClasses/Thief");
        characterData[index].setStatbyKey(PlayerKey.Skill1, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill2, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill3, "basicattack");
        characterData[index].setStatbyKey(PlayerKey.Skill4, "basicattack");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
