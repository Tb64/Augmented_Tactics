using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyList : MonoBehaviour
{

    private const float recrListDelta = -100f;

    public GameObject contentWindow;
    public GameObject UnitDisplayObj;

    private float[] slotXPos = { -180f, -60f, 60f, 180f };
    private Vector3 chrListPos;

    private PlayerData currentSelected;

    private List<PlayerData> army;
    private List<GameObject> playersInScrollview;

    void Start()
    {
        chrListPos = new Vector3(0f, 100f, 0f);
        playersInScrollview = new List<GameObject>();
    }

    public void LoadList<T>(List<PlayerData> army, T controller)
    {
        this.army = army;

        if (playersInScrollview.Count != 0)
        {
            foreach (GameObject gObj in playersInScrollview)
            {
                Destroy(gObj);
            }
        }

        playersInScrollview = new List<GameObject>();

        int index = 0;

        foreach (PlayerData pData in army)
        {
            Debug.Log("Adding char to recruit ui");
            //instantiate the unit display opbject, set its transform stuff
            GameObject obj = Instantiate<GameObject>(UnitDisplayObj);
            playersInScrollview.Add(obj);
            obj.transform.SetParent(contentWindow.transform, false);
            obj.GetComponent<RectTransform>().anchoredPosition3D = chrListPos;

            //get the RecruitButton script component off obj
            UnitDisplayButton btn = obj.GetComponent<UnitDisplayButton>();
            btn.LoadCharacter(pData);
            obj.GetComponent<Button>().onClick.AddListener(btn.ClickEvent);
            chrListPos.y += recrListDelta;
            index++;
        }
    }


    public virtual void ClickEvent()
    {
        //return
    }
}
