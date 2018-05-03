using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour {
    public Image[] inventoryImg;

    public Button BackPage;
    public Button NextPage;

    private int currentlySelected = -1;
    public Sprite nullImage;

    private List<UsableItem> items;
    private List<Armor> armors;
    private List<Weapons> weapons;

    public void UpdateInventory(List<UsableItem> list)
    {
        ResetUI();
        items = list;
        int index = 0;
        foreach (UsableItem item in items)
        {
            inventoryImg[index].sprite = Resources.Load<Sprite>(item.image);
            index++;
        }
    }

    public void UpdateInventory(List<Armor> list)
    {
        ResetUI();
        armors = list;
        int index = 0;
        foreach (Armor item in armors)
        {
            inventoryImg[index].sprite = Resources.Load<Sprite>(item.image);
            index++;
        }
    }

    public void UpdateInventory(List<Weapons> list)
    {
        ResetUI();
        weapons = list;
        int index = 0;
        foreach (Weapons item in weapons)
        {
            inventoryImg[index].sprite = Resources.Load<Sprite>(item.image);
            index++;
        }
    }

    public void ResetUI()
    {
        foreach (Image img in inventoryImg)
        {
            img.sprite = nullImage;
        }
    }

    public void ButtonClicked(int index)
    {
        currentlySelected = index;
        SendMessageUpwards("UpdateDetails", currentlySelected,SendMessageOptions.DontRequireReceiver);
    }

    public int GetCurrentSelecte()
    {
        return currentlySelected;
    }
}
