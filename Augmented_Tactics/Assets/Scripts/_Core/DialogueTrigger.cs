using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public TextAsset csvFile;
    public bool triggerOnlyOnce;
    private bool hasTriggered = false;
    private Dialogue dial = new Dialogue();

    public void triggerDialogue() {
        if(csvFile!=null)
            if (triggerOnlyOnce && !hasTriggered)
            {
                dial.startDialogue(csvFile.name);
                hasTriggered = true;
            }
            else if(!triggerOnlyOnce)
            {
                dial.startDialogue(csvFile.name);
            }
	}
}
