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

    public Image selectedAllSkillMarker;
    public Image selectedCurrentSkillMarker;

    public ArmyList armyListUI;

    public PlayerData selectedPlayer;

    private int allSkillSelected;
    private int currentSkillSelected;
    private Ability skill1, skill2, skill3, skill4;
    private Ability[] abilities;
    private GameObject dummy;
    private Actor dummyActor;

	// Use this for initialization
	void Start () {
        selectedPlayer = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        dummy = GameObject.FindGameObjectWithTag("Player");
        dummyActor = dummy.GetComponent<Actor>();
        GenerateUI();
        GameDataController.loadPlayerData();
        armyListUI.LoadList(GameDataController.gameData.armyList);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UnitButtonClicked(PlayerData pdata)
    {
        selectedPlayer = pdata;
        GenerateUI();
    }

    void GenerateUI()
    {
        //selectedPlayer.Class;
        dummyActor.LoadStatsFromData(selectedPlayer);
        string[] abilityKeys = SkillLoader.ClassSkills(selectedPlayer.Class);
        abilities = new Ability[8];

        for (int index = 0; index < 8; index++)
        {
            if(abilityKeys[index].Length != 0)
            {
                abilities[index] = SkillLoader.LoadSkill(abilityKeys[index], dummy);
                allSkills[index].sprite = abilities[index].abilityImage;
            }
        }

        if(selectedPlayer.Skill1 != null && selectedPlayer.Skill1.Length != 0)
        {
            skill1 = SkillLoader.LoadSkill(selectedPlayer.Skill1, dummy);
            currentSkills[0].sprite = skill1.abilityImage;
        }
        if (selectedPlayer.Skill2 != null && selectedPlayer.Skill2.Length != 0)
        {
            skill2 = SkillLoader.LoadSkill(selectedPlayer.Skill2, dummy);
            currentSkills[1].sprite = skill2.abilityImage;
        }
        if (selectedPlayer.Skill3 != null && selectedPlayer.Skill3.Length != 0)
        {
            skill3 = SkillLoader.LoadSkill(selectedPlayer.Skill3, dummy);
            currentSkills[2].sprite = skill3.abilityImage;
        }
        if (selectedPlayer.Skill4 != null && selectedPlayer.Skill4.Length != 0)
        {
            skill4 = SkillLoader.LoadSkill(selectedPlayer.Skill4, dummy);
            currentSkills[3].sprite = skill4.abilityImage;
        }

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

    }

    public void SetSelctedAllSkill(int input)
    {
        this.allSkillSelected = input;
        selectedAllSkillMarker.rectTransform.anchoredPosition3D = new Vector3(((input - 4f) * 100f) + 50f, 0f, 0f); //allSkills[input].rectTransform.anchoredPosition3D;
        //Debug.Log(allSkills[input].rectTransform.);
        this.nameText.text = abilities[input].abilityName;
        damageText.text = "Damage: " + abilities[input].damage;
        this.mpText.text = "Mana: " + abilities[input].manaCost;
        this.rangeText.text = "Range: " + abilities[input].range_min + "-" + abilities[input].range_max;
        this.descText.text = abilities[input].abilityDescription;

    }

    public void SetCurrentSkillSelected(int input)
    {
        this.currentSkillSelected = input;
        selectedCurrentSkillMarker.rectTransform.anchoredPosition3D = currentSkills[input].rectTransform.anchoredPosition3D;
    }
}
