using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootItem : MonoBehaviour {

    public Image icon;

    public void LoadItem(Weapons item)
    {
        icon.sprite = Resources.Load<Sprite>(item.image);
    }

    public void LoadItem(Armor item)
    {
        icon.sprite = Resources.Load<Sprite>(item.image);
    }

    public void LoadItem(UsableItem item)
    {
        icon.sprite = Resources.Load<Sprite>(item.image);
    }
}
