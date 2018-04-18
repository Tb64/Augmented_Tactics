using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectionMenu : MonoBehaviour {

    public Image[] allSkills;
    public Image[] currentSkills;

    public Text damageText;
    public Text mpText;
    public Text descText;
    public Text costText;

    public Image selectedAllSkillMarker;
    public Image selectedCurrentSkillMarker;

    public PlayerData selectedPlayer;

    private int allSkillSelected;
    private int currentSkillSelected;
    private GameObject dummy;
    private Actor dummyActor;

	// Use this for initialization
	void Start () {
        selectedPlayer = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        dummy = GameObject.FindGameObjectWithTag("Player");
        dummyActor = dummy.GetComponent<Actor>();
        GenerateUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateUI()
    {
        //selectedPlayer.Class;
        string[] abilityKeys = SkillLoader.ClassSkills(selectedPlayer.Class);
        Ability[] abilities = new Ability[8];

        for (int index = 0; index < 8; index++)
        {
            if(abilityKeys[index].Length != 0)
            {
                abilities[index] = SkillLoader.LoadSkill(abilityKeys[index], dummy);
                allSkills[index].sprite = abilities[index].abilityImage;
            }
        }


    }

    public void SetSelctedAllSkill(int input)
    {
        this.allSkillSelected = input;
        selectedAllSkillMarker.rectTransform.anchoredPosition3D = allSkills[input].rectTransform.anchoredPosition3D;
    }

    public void SetCurrentSkillSelected(int input)
    {
        this.currentSkillSelected = input;
        selectedCurrentSkillMarker.rectTransform.anchoredPosition3D = currentSkills[input].rectTransform.anchoredPosition3D;
    }
}
