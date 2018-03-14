using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployController : MonoBehaviour {

    private const float chrListDelta = -100f;

    public Image slotHighlight;

    public GameObject ListWindow;
    public GameObject PlayerListObj;

    public Image[] deployedImage;
    public PlayerData[] deployed;

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
        deployed = new PlayerData[4];
        chrListPos = new Vector3(0f,-60f,0f);
        LoadData();
        MakeList();

    }
	
	public void OnSlotSelected(int slot)
    {
        slotHighlight.rectTransform.anchoredPosition = new Vector2(slotXPos[slot],0f);
        slotSelected = slot;
    }

    void LoadData()
    {
        GameDataController.loadPlayerData();
        TEMP_CharacterList.Init();
        PlayerData data = TEMP_CharacterList.characterData[0];

        if (data == null)
            Debug.Log("Data failed to gerenate");

        if(GameDataController.gameData == null)
            Debug.Log("Data failed to make Game Date");
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[0]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[1]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[2]);
        GameDataController.gameData.addPlayer(TEMP_CharacterList.characterData[3]);

        army = GameDataController.gameData.getArmyList();
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
        currentSelected = input;
        Debug.Log("Setting slot #" + slotSelected + " to " + currentSelected.playerName);
        deployedImage[slotSelected].sprite = img;
    }

    public void setSelcted(PlayerData input)
    {
        currentSelected = input;
    }

}
