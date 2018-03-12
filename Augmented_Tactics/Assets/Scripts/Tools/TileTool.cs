using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTool : MonoBehaviour {


    public void SelectedWalkable(GameObject[] gObjects)
    {
        foreach (GameObject gObj in gObjects)
        {
            ClickableTile tile = gObj.GetComponent<ClickableTile>();
            if(tile != null)
            {
                tile.SetWalkable();
            }
        }
    }

    public void SelectedUnwalkable(GameObject[] gObjects)
    {
        foreach (GameObject gObj in gObjects)
        {
            ClickableTile tile = gObj.GetComponent<ClickableTile>();
            if (tile != null)
            {
                tile.SetUnwalkable();
            }
        }
    }
}
