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
        characterData[index] = PlayerData.GenerateNewPlayer();

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
        characterData[index] = PlayerData.GenerateNewPlayer();

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
        characterData[index] = PlayerData.GenerateNewPlayer();

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
        characterData[index] = PlayerData.GenerateNewPlayer();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public static void MakeParty()
    {
        PlayerData data1 = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        data1.Skill1 = "twinstrike";
        data1.Skill2 = "combo";
        data1.Skill3 = "dragonkick";
        data1.Skill4 = "shockwave";
        PlayerData data2 = PlayerData.GenerateNewPlayer(CharacterClasses.ClericKey);
        data2.Skill1 = "curewounds";
        data2.Skill2 = "healingword";
        data2.Skill3 = "beaconofhope";
        data2.Skill4 = "healingwinds";
        PlayerData data3 = PlayerData.GenerateNewPlayer(CharacterClasses.DarkKnightKey);
        data3.Skill1 = "sap";
        data3.Skill2 = "disintegrate";
        data3.Skill3 = "eviscerate";
        data3.Skill4 = "lifeleech";
        PlayerData data4 = PlayerData.GenerateNewPlayer(CharacterClasses.PaladinKey);
        data4.Skill1 = "vengeance";
        data4.Skill2 = "smite";
        data4.Skill3 = "aid";
        data2.Skill4 = "fortifiedstrike";
        PlayerData data5 = PlayerData.GenerateNewPlayer(CharacterClasses.ThiefKey);
        data5.Skill1 = "arrow";
        data5.Skill2 = "poisonarrow";
        data5.Skill3 = "flamingarrow";
        data5.Skill4 = "quickstab";
        PlayerData data6 = PlayerData.GenerateNewPlayer(CharacterClasses.MageKey);
        data6.Skill1 = "ice";
        data6.Skill2 = "thunder";
        data6.Skill3 = "magicmissle";
        data6.Skill4 = "fire";
        LevelUpMax(data1);
        LevelUpMax(data2);
        LevelUpMax(data3);
        LevelUpMax(data4);
        LevelUpMax(data5);
        LevelUpMax(data6);

    }

    public static void LevelUpMax(PlayerData input)
    {
        for (int index = 0; index < 8; index++)
        {
            PlayerData.LevelUp(input, true);
        }
    }
}
