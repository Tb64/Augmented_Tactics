using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public void ShowTiles()
    {
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        foreach (ClickableTile tile in loadedTiles)
        {
            tile.GetComponent<Renderer>().enabled = true;
        }
    }

    public void HideTiles()
    {
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        foreach (ClickableTile tile in loadedTiles)
        {
            tile.GetComponent<Renderer>().enabled = false;
        }
    }
}
