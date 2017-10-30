using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// This class manages the dialougue. To use it, attach the DialogueTrigger class to something; set the file name in the trigger script component
// and if it will activate only once. Ways of activating the script will vary. Clicking a button, timed event, any way to call the triggerDialogue method
public class Dialogue : MonoBehaviour {

    private static string filepath = "Assets/Resources/Dialogue/";
    private string dialoguePanelLoc = "CanvasWip/DialoguePanel";
    private string[] lines;
    private static ConvNode currentNode;
    private static GameObject dialoguePanel;
    private static GameObject[] buttonPrompts;
    private static GameObject buttonNextDialogue;
    private static Text[] textPrompts;
    private static Text textDialogue;

    void Start()
    {
        //be sure the dialogue panel is active before the scene starts or it won't be able to find the objects
        buttonPrompts = new GameObject[3];
        textPrompts = new Text[3];
        dialoguePanel = GameObject.Find(dialoguePanelLoc);
        textDialogue = GameObject.Find(dialoguePanelLoc + "/DialogueText").GetComponent<Text>();
        buttonNextDialogue = GameObject.Find(dialoguePanelLoc + "/NextDialogueButton");
        for (int i = 0; i < 3; i++)
        {
            buttonPrompts[i] = GameObject.Find(dialoguePanelLoc + "/PromptButton" + i.ToString());
            textPrompts[i] = GameObject.Find(dialoguePanelLoc + "/PromptButton" + i.ToString() + "/Text").GetComponent<Text>();
            buttonPrompts[i].SetActive(false);
        }
        dialoguePanel.SetActive(false);
    }


    public void startDialogue(string fileName)
    {
        //open csv and get all in a tree
        try
        {
            lines = System.IO.File.ReadAllText(filepath + fileName + ".csv").Split('\n');
            ConvNode startNode = new ConvNode();
            buildTree(startNode, 1);
            currentNode = startNode;
            dialoguePanel.SetActive(true);
            if (currentNode != null)
                displayText();
            else
                Debug.Log("Current Node is null, something broke!");
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString());
        }        
    }
    
    public void displayText()
    {
        if (currentNode != null)
            textDialogue.text = currentNode.reply;
    }

    public void promptSelection(int choice)
    {
        currentNode = currentNode.links[choice];
        for (int i = 0; i < 3; i++)
            buttonPrompts[i].SetActive(false);
        buttonNextDialogue.SetActive(true);
        displayText();
    }

    public void nextDialogue()
    {

        //No prompts. Either continue to next node or end of convo
        
        if (currentNode != null && (currentNode.prompts[0] == null || currentNode.prompts[0] == ""))
        {
            //if there is a link continue, else dialogue over
            if (currentNode.links[0] != null)
            {
                currentNode = currentNode.links[0];
                displayText();
            }
            else
            {
                dialoguePanel.SetActive(false);
            }
        }
        else if (currentNode != null)
        {
            //else branch. show as many buttons as options
            //activate buttons
            buttonNextDialogue.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                if (currentNode.prompts[i] != null)
                {
                    buttonPrompts[i].SetActive(true);
                    textPrompts[i].text = currentNode.prompts[i];
                }
            }
        }
    }


    public void buildTree(ConvNode cNode, int currentLine)
    {
        int nextLine, j = 0;
        //break each element into array
        string[] linesData = lines[currentLine - 1].Trim().Split(',');
        
        cNode.reply = linesData[j];
        j++;
        //while this index isn't out of array bounds, and there is a line number next that is parsed sucessfully
        //expected to read a pattern of line number, then prompt. 
        while (j < linesData.Length && (System.Int32.TryParse(linesData[j], out nextLine)))
        {
            //j expected to be 1, 3, 5... j/2 will be 0, 1, 2
            ConvNode newNode = new ConvNode();
            cNode.links[j/2] = newNode;
            buildTree(newNode, nextLine);
            j++;
            //if there is no prompt assume finished, note j will be 2,4,6, must subtract 1 from (j/2) for array
            if (j < linesData.Length && linesData[j] != null) 
                cNode.prompts[(j/2)-1] = linesData[j];
            else
                return;
            j++;
        }
    }
}

public class ConvNode{
    public string reply; //what will be displayed as said
    public string[] prompts = new string[3]; //possible promts for each
    public ConvNode[] links = new ConvNode[3];
}