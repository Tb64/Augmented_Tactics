using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployController : MonoBehaviour {

    private const float chrListDelta = -100f;

    public Image slotHighlight;

    public GameObject ListWindow;
    public GameObject PlayerListObj;

    private Sprite emptySlot;
    public Image[] deployedImage;
    public Transform[] modelSpawns;
    private PlayerData[] deployed;
    private GameObject[] models;

    public ArmyList armyListUI;

    private int slotSelected = 0;

    private float[] slotXPos = {-180f,-60f,60f,180f };
    private Vector3 chrListPos;

    private PlayerData currentSelected;

    private List<PlayerData> army;

    private void Awake()
    {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
    }

    // Use this for initialization
    void Start () {
        emptySlot = deployedImage[0].sprite; //
        deployed = new PlayerData[4];
        chrListPos = new Vector3(0f,-60f,0f);
        models = new GameObject[4];
        LoadData();
        //MakeList();

    }
	
	public void OnSlotSelected(int slot)
    {
        slotHighlight.rectTransform.anchoredPosition = new Vector2(slotXPos[slot],0f);
        slotSelected = slot;
    }

    void LoadData()
    {
        GameDataController.loadPlayerData();
        //TEMP_CharacterList.Init();
        //PlayerData data = TEMP_CharacterList.characterData[0];

        //if (data == null)
        //    Debug.Log("Data failed to gerenate");

        //if(GameDataController.gameData == null)
        //{
        //    Debug.Log("Data failed to make Game Date");
        //    GameDataController.gameData = new GameData();
        //}
        //GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[0]);
        //GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[1]);
        //GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[2]);
        //GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[3]);

        army = GameDataController.gameData.getArmyList();
        armyListUI.LoadList(army);
        //GameDataController.savePlayerData();
    }

    private void MakeList()
    {
        int index = 0;
        foreach(PlayerData pData in army)
        {
            Debug.Log("Adding char to list");
            GameObject obj = Instantiate<GameObject>(PlayerListObj);
            obj.transform.SetParent(ListWindow.transform,false);
            obj.GetComponent<RectTransform>().anchoredPosition3D = chrListPos;
            DeployCharacterButton btn = obj.GetComponent<DeployCharacterButton>();
            btn.LoadCharacter(pData,this);
            obj.GetComponent<Button>().onClick.AddListener(btn.ChangeSelected);
            chrListPos.y += chrListDelta;
            index++;
        }
    }

    public void ChangeSelected()
    {
        Debug.Log("Clicked on " + name);
    }

    public void ChangeSelected(PlayerData input, Sprite img)
    {
        int slot = PlayerAlreadyDeployed(input);
        currentSelected = input;
        if (slot == -1)
        {
            Debug.Log("Setting slot #" + slotSelected + " to " + currentSelected.playerName);
            SetSlot(input, img, slot);
        }
        else
        {
            deployedImage[slot].sprite = emptySlot;
            deployed[slot] = null;
            GameObject.Destroy(models[slot]);

            Debug.Log("Moving slot #" + slot + " to #" + slotSelected + " for " + currentSelected.playerName);
            SetSlot(input, img, slot);
        }
    }

    public void ChangeSelected(PlayerData input)
    {
        Sprite img = Resources.Load<Sprite>(input.Icon);
        ChangeSelected(input, img);
    }

    private void SetSlot(PlayerData input, Sprite img, int slot)
    {
        deployedImage[slotSelected].sprite = img;
        deployed[slotSelected] = input;
        GameObject model = Resources.Load<GameObject>(input.getStringByKey(PlayerKey.Prefab));
        model.GetComponent<PlayerControlled>().combatOn = false;
        GameObject gObj = Instantiate<GameObject>(model);
        gObj.transform.SetPositionAndRotation(modelSpawns[slotSelected].position, modelSpawns[slotSelected].rotation);
        GameObject.Destroy(models[slotSelected]);
        models[slotSelected] = gObj;
    }

    public void setSelcted(PlayerData input)
    {
        currentSelected = input;
    }

    private int PlayerAlreadyDeployed(PlayerData input)
    {
        for (int index = 0; index < deployed.Length; index++)
        {
            if(deployed[index] != null && input.playerName == deployed[index].playerName)
                return index;
        }

        return -1;
    }

    public void SaveDeployed()
    {
        GameDataController.loadPlayerData();
        for (int index = 0; index < 4; index++)
        {
            GameDataController.gameData.currentTeam[index] = this.deployed[index];
        }
        GameDataController.savePlayerData();
    }

    public void SaveThenLoad(string levelName)
    {
        SaveDeployed();


    }

    public void UnitButtonClicked(PlayerData pdata)
    {
        currentSelected = pdata;
        ChangeSelected(pdata);
        //GenerateUI();
    }
}