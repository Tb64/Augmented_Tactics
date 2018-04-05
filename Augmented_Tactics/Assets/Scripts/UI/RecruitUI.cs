using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitUI : MonoBehaviour
{

    private const float recrListDelta = -100f;

    public Text shards;
    public Text currentUnitsOwned;
    public Text selectedName;
    public Text selectedClass;
    public Text selectedCost;

    public Image slotHighlight;

    public GameObject ListWindow;
    public GameObject UnitDisplayObj;
    public GameObject cameraSetViewObj;
    public GameObject modelTansformObj;

    public Image[] equipmentSlots;
    public Image[] itemSlots;
    public Image[] skillsSlots;
    public Image[] deployedImage;
    public PlayerData[] deployed;

    private int slotSelected = 0;

    private float[] slotXPos = { -180f, -60f, 60f, 180f };
    private Vector3 chrListPos;

    private PlayerData currentSelected;

    private List<PlayerData> army;

    private GameObject modelObj;

    private void Awake()
    {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
    }

    // Use this for initialization
    void Start()
    {
        deployed = new PlayerData[5];
        deployedImage = new Image[5];
        chrListPos = new Vector3(0f, 0f, 0f);
        GameObject obj = Instantiate<GameObject>(cameraSetViewObj);
        LoadData();
        MakeList();
    }

    public void OnSlotSelected(int slot)
    {
        slotHighlight.rectTransform.anchoredPosition = new Vector2(slotXPos[slot], 0f);
        slotSelected = slot;
    }

    void LoadData()
    {
        GameDataController.loadPlayerData();
        TEMP_CharacterList.Init();
        PlayerData data = TEMP_CharacterList.characterData[0];

        if (data == null)
            Debug.Log("Data failed to gerenate");

        if (GameDataController.gameData == null)
            Debug.Log("Data failed to make Game Date");
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[0]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[1]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[2]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[3]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[0]);
        army = GameDataController.gameData.getArmyList();

        //load current shards and units in army
    }

    //this fills the list with loaded objects, in the test case it loads 5 temp chars
    private void MakeList()
    {
        int index = 0;
        foreach (PlayerData pData in army)
        {
            Debug.Log("Adding char to recruit ui");
            //instantiate the unit display opbject, set its transform stuff
            GameObject obj = Instantiate<GameObject>(UnitDisplayObj);
            obj.transform.SetParent(ListWindow.transform, false);
            obj.GetComponent<RectTransform>().anchoredPosition3D = chrListPos;

            //get the RecruitButton script component off obj
            RecruitButton btn = obj.GetComponent<RecruitButton>();
            btn.LoadCharacter(pData, this);
            obj.GetComponent<Button>().onClick.AddListener(btn.ChangeSelected);
            chrListPos.y += recrListDelta;
            index++;
        }
    }

    public void ChangeSelected()
    {
        //Debug.Log("Clicked on " + name);
    }

    public void ChangeSelected(PlayerData input, Sprite img)
    {
        currentSelected = input;
        //Debug.Log("Setting slot #" + slotSelected + " to " + currentSelected.playerName);
        //load character into soldier view, load name/cost/class
        //load equipment, item, skills
        selectedName.text = "Name: " + input.getStringByKey(PlayerKey.DisplayName);
        selectedCost.text = "Cost: " + 500;
        selectedClass.text = "Class: " + input.getStringByKey(PlayerKey.ClassName);
        foreach (Image skillImg in skillsSlots) {
            //get a string for each skill, apply an image to it
            string skillStr = input.getStringByKey(PlayerKey.Icon);
            //string skillStr = "sword";
            Debug.Log("setting an image for skills: "+ skillStr);
            skillImg.sprite = Resources.Load<Sprite>(skillStr);
        }

        //load model into soldierview, if one is active, destory it.
        if (modelObj != null) { Destroy(modelObj)};
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

}
