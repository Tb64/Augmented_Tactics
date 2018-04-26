using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectionMenu : MonoBehaviour {

    public Image[] allSkills;
    public Image[] currentSkills;
    public Image[] lockedSkills;
    public Image[] purchasedSkills;

    public Text nameText;
    public Text damageText;
    public Text mpText;
    public Text rangeText;
    public Text descText;
    public Text costText;
    public Text buyText;

    public Image selectedAllSkillMarker;
    public Image selectedCurrentSkillMarker;

    public ArmyList armyListUI;

    public PlayerData selectedPlayer;

    private int allSkillSelected;
    private int currentSkillSelected;
    private Ability skill1, skill2, skill3, skill4;
    private Ability[] abilities;
    private string[] abilityKeys;
    public GameObject dummy;
    private Actor dummyActor;

    public Sprite nullImage;

	// Use this for initialization
	void Start () {
        //nullImage = currentSkills[0].sprite;
        if (selectedPlayer == null)
            selectedPlayer = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        SetPlayerData(selectedPlayer);
        
    }

    public void SetPlayerData(PlayerData pdata)
    {
        selectedPlayer = pdata;
        dummyActor = dummy.GetComponent<Actor>();
        GenerateUI();
        GameDataController.loadPlayerData();
        armyListUI.LoadList(GameDataController.gameData.armyList);
        SetSelctedAllSkill(0);
    }

    public void UnitButtonClicked(PlayerData pdata)
    {
        selectedPlayer = pdata;
        SetPlayerData(pdata);
    }

    public void BuyButtonClicked()
    {
        if (buyText.text.Contains("Level"))
            return;
        else if (buyText.text.Contains("Buy"))
        {
            UnlockSkill(allSkillSelected);
            GameDataController.gameData.savePlayer(selectedPlayer);
            GameDataController.savePlayerData();
        }
        else if (buyText.text.Contains("Set"))
        {
            if (IsSkillUnlocked(allSkillSelected))
            {
                SetSkill(abilityKeys[allSkillSelected], currentSkillSelected);
                GameDataController.gameData.savePlayer(selectedPlayer);
                GameDataController.savePlayerData();
            }
        }
        GenerateUI();
    }

    void GenerateUI()
    {
        //selectedPlayer.Class;

        foreach (Image img in currentSkills)
        {
            img.sprite = nullImage;
        }

        dummyActor.LoadStatsFromData(selectedPlayer);
        abilityKeys = SkillLoader.ClassSkills(selectedPlayer.Class);
        abilities = new Ability[8];

        for (int index = 0; index < 8; index++)
        {
            if(abilityKeys[index].Length != 0)
            {
                abilities[index] = SkillLoader.LoadSkill(abilityKeys[index], dummy);
                allSkills[index].sprite = abilities[index].abilityImage;
            }
        }

        if (selectedPlayer.Skill1 != null && selectedPlayer.Skill1.Length != 0)
        {
            skill1 = SkillLoader.LoadSkill(selectedPlayer.Skill1, dummy);
            currentSkills[0].sprite = skill1.abilityImage;
        }
        else
            currentSkills[0].sprite = nullImage;
        if (selectedPlayer.Skill2 != null && selectedPlayer.Skill2.Length != 0)
        {
            skill2 = SkillLoader.LoadSkill(selectedPlayer.Skill2, dummy);
            currentSkills[1].sprite = skill2.abilityImage;
            Debug.Log("Skill2 = " + skill2.abilityName);
        }
        else
            currentSkills[1].sprite = nullImage;
        if (selectedPlayer.Skill3 != null && selectedPlayer.Skill3.Length != 0)
        {
            skill3 = SkillLoader.LoadSkill(selectedPlayer.Skill3, dummy);
            currentSkills[2].sprite = skill3.abilityImage;
        }
        else
            currentSkills[2].sprite = nullImage;

        if (selectedPlayer.Skill4 != null && selectedPlayer.Skill4.Length != 0)
        {
            skill4 = SkillLoader.LoadSkill(selectedPlayer.Skill4, dummy);
            currentSkills[3].sprite = skill4.abilityImage;
        }
        else
            currentSkills[3].sprite = nullImage;
        for (int index = 0; index < selectedPlayer.Level; index++)
        {
            lockedSkills[index].enabled = false;
        }

        purchasedSkills[0].enabled = selectedPlayer.UnlockSkill1;
        purchasedSkills[1].enabled = selectedPlayer.UnlockSkill2;
        purchasedSkills[2].enabled = selectedPlayer.UnlockSkill3;
        purchasedSkills[3].enabled = selectedPlayer.UnlockSkill4;
        purchasedSkills[4].enabled = selectedPlayer.UnlockSkill5;
        purchasedSkills[5].enabled = selectedPlayer.UnlockSkill6;
        purchasedSkills[6].enabled = selectedPlayer.UnlockSkill7;
        purchasedSkills[7].enabled = selectedPlayer.UnlockSkill8;
        //lockedSkills[0].enabled = !selectedPlayer.UnlockSkill1;

        if (allSkillSelected >= selectedPlayer.Level)
            buyText.text = "Level " + (allSkillSelected + 1);
        else
        {
            if (IsSkillUnlocked(allSkillSelected))
                buyText.text = "Set Skill";
            else
                buyText.text = "Buy";
        }
        

    }

    public void SetSelctedAllSkill(int input)
    {
        this.allSkillSelected = input;
        selectedAllSkillMarker.rectTransform.anchoredPosition3D = new Vector3(((input - 4f) * 100f) + 50f, 0f, 0f); //allSkills[input].rectTransform.anchoredPosition3D;
        //Debug.Log(allSkills[input].rectTransform.);
        if(abilities[input] == null)
        {
            this.nameText.text = "Null";
            this.damageText.text = "";
            this.mpText.text = "";
            this.rangeText.text = "";
            this.descText.text = "";
            return;
        }
        this.nameText.text = abilities[input].abilityName;
        this.damageText.text = "Damage: " + abilities[input].damage;
        this.mpText.text = "Mana: " + abilities[input].manaCost;
        this.rangeText.text = "Range: " + abilities[input].range_min + "-" + abilities[input].range_max;
        this.descText.text = abilities[input].abilityDescription;
        if (input >= selectedPlayer.Level)
            buyText.text = "Level " + (input + 1);
        else
        {
            if(IsSkillUnlocked(input))
                buyText.text = "Set Skill";
            else
                buyText.text = "Buy";
        }
        GenerateUI();
    }

    public void SetCurrentSkillSelected(int input)
    {
        this.currentSkillSelected = input;
        selectedCurrentSkillMarker.rectTransform.anchoredPosition3D = new Vector3(((input - 2f) * 110f) + 55f, 0f, 0f);
    }

    private bool IsSkillUnlocked(int index)
    {
        switch (index)
        {
            case 0:
                return selectedPlayer.UnlockSkill1;
            case 1:
                return selectedPlayer.UnlockSkill2;
            case 2:
                return selectedPlayer.UnlockSkill3;
            case 3:
                return selectedPlayer.UnlockSkill4;
            case 4:
                return selectedPlayer.UnlockSkill5;
            case 5:
                return selectedPlayer.UnlockSkill6;
            case 6:
                return selectedPlayer.UnlockSkill7;
            case 7:
                return selectedPlayer.UnlockSkill8;

            default:
                return true;
        }
    }
    private void UnlockSkill(int index)
    {
        switch (index)
        {
            case 0:
                selectedPlayer.UnlockSkill1 = true;
                return;
            case 1:
                selectedPlayer.UnlockSkill2 = true;
                return;
            case 2:
                selectedPlayer.UnlockSkill3 = true;
                return;
            case 3:
                selectedPlayer.UnlockSkill4 = true;
                return;
            case 4:
                selectedPlayer.UnlockSkill5 = true;
                return;
            case 5:
                selectedPlayer.UnlockSkill6 = true;
                return;
            case 6:
                selectedPlayer.UnlockSkill7 = true;
                return;
            case 7:
                selectedPlayer.UnlockSkill8 = true;
                return;

            default:
                return;
        }
    }

    private void SetSkill(string name, int slot)
    {
        switch (slot)
        {
            case 0:
                selectedPlayer.Skill1 = name;
                return;
            case 1:
                selectedPlayer.Skill2 = name;
                return;
            case 2:
                selectedPlayer.Skill3 = name;
                return;
            case 3:
                selectedPlayer.Skill4 = name;
                return;
            default:
                break;
        }
    }
}
