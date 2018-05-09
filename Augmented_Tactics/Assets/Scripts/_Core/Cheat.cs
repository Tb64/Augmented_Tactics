using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

	public GameObject lamp1;
    public GameObject lamp2;
    public GameObject lamp3;

    public int lamp1Counter;
    public int lamp2Counter;
    public int lamp3Counter;
    public GameObject cheatHead;
    public bool barrel;
    public GameObject storeUI;

    GameObject RayCaster()
    {

        GameObject interactedObject;
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            interactedObject = interactionInfo.collider.gameObject;
            Debug.Log("Click event on: " + interactedObject.name);
            return interactedObject;
        }
        return null;
    }

    private void OnMouseUp()
    {
        RayCaster();

        if (lamp1 != null)
        {
            cheatHead.GetComponent<Cheat>().lamp1Counter++;
            if(cheatHead.GetComponent<Cheat>().lamp1Counter > 3)
                resetCounters();
        }
        else if (lamp2 != null)
        {
            cheatHead.GetComponent<Cheat>().lamp2Counter++;
            if (cheatHead.GetComponent<Cheat>().lamp2Counter > 3)
                resetCounters();
        }
        else if (lamp3 != null)
        {
            cheatHead.GetComponent<Cheat>().lamp3Counter++;
            if (cheatHead.GetComponent<Cheat>().lamp3Counter > 3)
                resetCounters();
        }


        if (barrel == true){
            if (cheatHead.GetComponent<Cheat>().checkLampCounters() == true)
            {
                if (storeUI.activeSelf == false)
                {
                    storeUI.SetActive(true);
                }

            }
        }
    }


    public bool checkLampCounters()
    {

        if (cheatHead.GetComponent<Cheat>().lamp1Counter == 3 &&
            cheatHead.GetComponent<Cheat>().lamp2Counter == 3 &&
            cheatHead.GetComponent<Cheat>().lamp3Counter == 3)
        {

            return true;
        }
        return false;
    }

    private void resetCounters()
    {
        cheatHead.GetComponent<Cheat>().lamp1Counter = 0;
        cheatHead.GetComponent<Cheat>().lamp2Counter = 0;
        cheatHead.GetComponent<Cheat>().lamp3Counter = 0;
    }

}
