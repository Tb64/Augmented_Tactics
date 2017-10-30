using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public string fileName;
    public bool triggerOnlyOnce;
    private bool hasTriggered = false;
    private Dialogue dial = new Dialogue();

    public void triggerDialogue() {
        if (triggerOnlyOnce && !hasTriggered)
        {
            dial.startDialogue(fileName);
            hasTriggered = true;
        }
        else if(!triggerOnlyOnce)
        {
            dial.startDialogue(fileName);
        }
	}
}
