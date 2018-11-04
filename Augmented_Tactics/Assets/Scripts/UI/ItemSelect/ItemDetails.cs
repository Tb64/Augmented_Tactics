using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour {

    public Image icon;
    public Text desc;

    /// <summary>
    /// TThis method loads the details of the passed usable item item the details viewer
    /// </summary>
    /// <param name="item"></param>
    public void LoadItem(UsableItem item)
    {
        icon.sprite = Resources.Load<Sprite>(item.image);

        string details = "";
        details += item.name + "\n\n";
        details += item.itemDesc + "\n";
        details += "Cost: " + item.cost + "\n";

        desc.text = details;
    }

}
