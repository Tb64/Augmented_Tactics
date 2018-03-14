using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployController : MonoBehaviour {

    public Image slotHighlight;

    private int slotSelected = 0;

    private float[] slotXPos = {-180f,-60f,60f,180f };

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnSlotSelected(int slot)
    {
        slotHighlight.rectTransform.anchoredPosition = new Vector2(slotXPos[slot],0f);
        slotSelected = slot;
    }
}
