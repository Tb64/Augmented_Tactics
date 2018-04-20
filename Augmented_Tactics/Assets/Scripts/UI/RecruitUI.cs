using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitUI : UnitDisplayButton
{

    private const float recrListDelta = -100f;

    public int charactersToLoad;
    public Text shards;
    public Text currentUnitsOwned;

    public Image slotHighlight;

    public GameObject statsPanel;
    public GameObject ListWindow;
    public GameObject UnitDisplayObj;
    public GameObject cameraSetViewObj;
    public GameObject modelTansformObj;
    public GameObject armyListObj;

    public Image[] equipmentSlots;
    public Image[] itemSlots;
    public Image[] skillsSlots;

    private int slotSelected = 0;

    private float[] slotXPos = { -180f, -60f, 60f, 180f };
    private Vector3 chrListPos;

    private PlayerData currentSelected;

    private List<PlayerData> army;
    private List<PlayerData> recruits;
    private List<GameObject> recruitsInList;

    private GameObject modelObj;

    private void Awake()
    {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
    }

    // Use this for initialization
    void Start()
    {
        chrListPos = new Vector3(0f, 100f, 0f);
        GameObject obj = Instantiate<GameObject>(cameraSetViewObj);
        recruitsInList = new List<GameObject>();
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
        armyListObj.GetComponent<ArmyList>().LoadList(playerList, this);
    }
    
    public void UnitButtonClicked(PlayerData data)
    {
        ChangeSelected(data);
    }



    public void ChangeSelected(PlayerData input)
    {
        currentSelected = input;

        Text[] textComponents = statsPanel.GetComponentsInChildren<Text>();

        //get the info for the character
        foreach (Text textComp in textComponents)
        {
            if(textComp.name == "Health")
                textComp.text = "Health: " + input.getStatByKey(PlayerKey.Health);
            else if (textComp.name == "Mana")
                textComp.text = "Mana: " + input.getStatByKey(PlayerKey.Mana);
            else if (textComp.name == "Strength")
                textComp.text = "Strength: " + input.getStatByKey(PlayerKey.Strength);
            else if (textComp.name == "Dexterity")
                textComp.text = "Dexterity: " + input.getStatByKey(PlayerKey.Dexterity);
            else if (textComp.name == "Constitution")
                textComp.text = "Constitution: " + input.getStatByKey(PlayerKey.Constitution);
            else if (textComp.name == "Intelligence")
                textComp.text = "Intelligence: " + input.getStatByKey(PlayerKey.Intelligence);
            else if (textComp.name == "Wisdom")
                textComp.text = "Wisdom: " + input.getStatByKey(PlayerKey.Wisdom);
        }

        //get a string for each skill, apply an image to it
        foreach (Image skillImg in skillsSlots)
        {
            string skillStr = input.getStringByKey(PlayerKey.Icon);
            Debug.Log("setting an image for skills: "+ skillStr);
            skillImg.sprite = Resources.Load<Sprite>(skillStr);
        }

       

        //load model into soldierview, if one is active, destory it.
        if (modelObj != null) { Destroy(modelObj); }
        GameObject model = Resources.Load<GameObject>(input.getStringByKey(PlayerKey.Prefab));
        model.GetComponent<PlayerControlled>().combatOn = false;
        modelObj = Instantiate<GameObject>(model);
        modelObj.transform.localScale = modelTansformObj.transform.lossyScale;
        modelObj.transform.SetPositionAndRotation(modelTansformObj.transform.position, modelTansformObj.transform.rotation);
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
}
