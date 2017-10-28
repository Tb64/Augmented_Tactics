using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// This class will handle loading of text
public class Dialogue : MonoBehaviour {

    [SerializeField]
    private string fileName;
    private static string filepath = "Assets/Resources/Dialogue/";
    private string[] lines;
    private ConvNode currentNode;
    public Text dialogueText;


    void Start()
    {
        //open csv and get all in a tree
        lines = System.IO.File.ReadAllText(filepath + fileName + ".csv").Split('\n');
        ConvNode startNode = new ConvNode();
        buildTree(startNode, 1);
        currentNode = startNode;

        displayText();
    }
    
    public void displayText()
    {
        Debug.Log(currentNode.reply);
        if (currentNode != null)
        {
            Debug.Log("worked");
            dialogueText.text = currentNode.reply;
        }
    }

    public void displayPromts()
    {

    }

    public void nextDialogue()
    {

        //will it branch? If no prompt then will not branch
        /*if (currentNode.prompts[0] == null)
        { 
            currentNode = currentNode.links[0];
            Debug.Log(currentNode.reply);
            displayText();
        }
        else
        {

        }*/
    }
    
    public void buildTree(ConvNode cNode, int currentLine)
    {
        int nextLine, j = 0;
        //break each element into array
        string[] linesData = lines[currentLine - 1].Trim().Split(',');
        
        cNode.reply = linesData[j];
        j++;
        //while this index isn't out of array bounds, and there is a line number next that is parsed sucessfully
        //expected to read a pattern of line number, then prompt
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