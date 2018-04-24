using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitUI : UnitDisplayButton
{

    private const float recrListDelta = -100f;


    public Sprite defaultImage;
    public int charactersToLoad;
    public Text shards;
    public Text currentUnitsOwned;

    public Image slotHighlight;

    public GameObject statsPanel;
    public Transform modelTansform;
    public GameObject armyListObj;
    public EquipStatsUI equipStatsUI;
    public GameObject dummy;
    //public GameObject ListWindow;
    //public GameObject UnitDisplayObj;
    //public GameObject cameraSetViewObj;

    public Image[] skillsSlots;
    public Image[] equipmentSlots;
    //public Image[] itemSlots;

    //private int slotSelected = 0;

    //private float[] slotXPos = { -180f, -60f, 60f, 180f };
    //private Vector3 chrListPos;

    private PlayerData currentSelected;

    private List<PlayerData> recruits;

    private string[] initialTextValues;
    //private List<PlayerData> army;
    //private List<GameObject> recruitsInList;

    private GameObject modelObj;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        initialTextValues = new string[10];
        //save the initial text values to reset everything later
        foreach (Text textComp in statsPanel.GetComponentsInChildren<Text>())
        {
            if (textComp.name == "Health")
                initialTextValues[0] = textComp.text;
            else if (textComp.name == "Mana")
                initialTextValues[1] = textComp.text;
            else if (textComp.name == "Experience")
                initialTextValues[2] = textComp.text;
            else if (textComp.name == "Strength")
                initialTextValues[3] = textComp.text;
            else if (textComp.name == "Dexterity")
                initialTextValues[4] = textComp.text;
            else if (textComp.name == "Constitution")
                initialTextValues[5] = textComp.text;
            else if (textComp.name == "Intelligence")
                initialTextValues[6] = textComp.text;
            else if (textComp.name == "Wisdom")
                initialTextValues[7] = textComp.text;

        }
        LoadData();
        MakeList(recruits);
    }

    void LoadData()
    {
        recruits = new List<PlayerData>();
        GameDataController.loadPlayerData();

        if (GameDataController.gameData == null)
            Debug.Log("Data failed to make Game Date");
        for (int i = 0; i < charactersToLoad; i++)
        {
            recruits.Add(PlayerData.GenerateNewPlayer());
        }
        
    }

    //this fills the list with loaded objects
    private void MakeList(List<PlayerData> playerList)
    {
        armyListObj.GetComponent<ArmyList>().LoadList(playerList);
    }
    
    public void UnitButtonClicked(PlayerData data)
    {
        ChangeSelected(data);
    }



    public void ChangeSelected(PlayerData input)
    {
        currentSelected = input;

        Text[] textComponents = statsPanel.GetComponentsInChildren<Text>();
        Actor dummyActor = dummy.GetComponent<Actor>();
        dummyActor.LoadStatsFromData(input);

        //get the info for the character
        foreach (Text textComp in textComponents)
        {
            if(textComp.name == "Health")
                textComp.text = "Health: " + input.Health;
            else if (textComp.name == "Mana")
                textComp.text = "Mana: " + input.Mana;
            else if (textComp.name == "Experience")
                textComp.text = "Experience: " + input.Experience;
            else if (textComp.name == "Strength")
                textComp.text = "Strength: " + input.Strength;
            else if (textComp.name == "Dexterity")
                textComp.text = "Dexterity: " + input.Dexterity;
            else if (textComp.name == "Constitution")
                textComp.text = "Constitution: " + input.Constitution;
            else if (textComp.name == "Intelligence")
                textComp.text = "Intelligence: " + input.Intelligence;
            else if (textComp.name == "Wisdom")
                textComp.text = "Wisdom: " + input.Wisdom;
        }

        for (int i = 0; i < 4; i++)
        {
            if (dummyActor.abilitySet[i] != null && dummyActor.abilitySet[i].abilityImage != null)
                skillsSlots[i].sprite = dummyActor.abilitySet[i].abilityImage;
            else
                skillsSlots[i].sprite = defaultImage;
        }
       
        //load model into soldierview, if one is active, destory it.
        if (modelObj != null) { Destroy(modelObj); }
        GameObject model = Resources.Load<GameObject>(input.getStringByKey(PlayerKey.Prefab));
        model.GetComponent<PlayerControlled>().combatOn = false;
        modelObj = Instantiate<GameObject>(model);
        modelObj.transform.localScale = modelTansform.lossyScale;
        modelObj.transform.SetPositionAndRotation(modelTansform.position, modelTansform.rotation);
    }

    public void setSelcted(PlayerData input)
    {
        currentSelected = input;
    }

    public void BuySelected()
    {
        recruits.Remove(currentSelected);
        MakeList(recruits);
        if (modelObj != null) { Destroy(modelObj); }
        GameDataController.gameData.addPlayer(currentSelected);
        GameDataController.savePlayerData();
    }
    public void WeaponClick()
    {
        equipStatsUI.DrawStats(currentSelected.weapon);
    }
    public void ArmorClick()
    {
        equipStatsUI.DrawStats(currentSelected.armor);
    }

    //this is to reset the recruit ui
    private void OnDisable()
    {
        if (modelObj != null) { Destroy(modelObj); }

        foreach (Text textComp in statsPanel.GetComponentsInChildren<Text>())
        {
            if (textComp.name == "Health")
                textComp.text = initialTextValues[0];
            else if (textComp.name == "Mana")
                textComp.text = initialTextValues[1];
            else if (textComp.name == "Experience")
                textComp.text = initialTextValues[2];
            else if (textComp.name == "Strength")
                textComp.text = initialTextValues[3];
            else if (textComp.name == "Dexterity")
                textComp.text = initialTextValues[4];
            else if (textComp.name == "Constitution")
                textComp.text = initialTextValues[5];
            else if (textComp.name == "Intelligence")
                textComp.text = initialTextValues[6];
            else if (textComp.name == "Wisdom")
                textComp.text = initialTextValues[7];
        }
        foreach (Image slot in skillsSlots)
            slot.sprite = defaultImage;
        foreach (Image slot in equipmentSlots)
            slot.sprite = defaultImage;
    }
}
