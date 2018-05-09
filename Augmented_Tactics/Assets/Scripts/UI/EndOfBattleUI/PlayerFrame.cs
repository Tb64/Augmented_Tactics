using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrame : MonoBehaviour {

    public Image icon;
    public Text nameText;
    public Text levelText;
    public Text expText;
    public Slider expBar;

    private int oldExp;
    private int newExp;
    private int currentLevel;

    private int expUpdate;

    public void LoadData(PlayerData data, int expGained)
    {
        if(data.playerName == "")
        {
            gameObject.SetActive(false);
            return;
        }
        currentLevel = data.Level;
        icon.sprite = Resources.Load<Sprite>(data.Icon);
        nameText.text = "Name: " + data.DisplayName;
        levelText.text = "Level: " + currentLevel;

        oldExp = data.Experience;
        newExp = oldExp + expGained;

        expUpdate = expGained / 100;

    }

    private void Update()
    {
        ExpUpdate();
    }

    private float ExpSliderCalc(int exp, int level)
    {
        if (level == 8)
            return 1f;
        int oldCap = PlayerKey.LevelCaps[currentLevel - 1];
        int nextCap = PlayerKey.LevelCaps[currentLevel];
        int expCap = nextCap - oldCap;
        int expLevel = exp - oldCap;

        float output = Mathf.Clamp01((float)expLevel / (float)expCap);
        //Debug.Log(output);
        return output;
    }

    private void ExpUpdate()
    {
        if (oldExp >= newExp)
            oldExp = newExp;
        else
            oldExp += expUpdate;

        
        if (oldExp >= PlayerKey.LevelCaps[currentLevel])
        {
            currentLevel++;
        }
        expBar.value = ExpSliderCalc(oldExp,currentLevel);
        if (currentLevel >= PlayerKey.MAX_LEVEL)
            expText.text = "MAX LEVEL";
        else
            expText.text = oldExp + "/" + PlayerKey.LevelCaps[currentLevel];
        levelText.text = "Level: " + currentLevel;
    }

}
