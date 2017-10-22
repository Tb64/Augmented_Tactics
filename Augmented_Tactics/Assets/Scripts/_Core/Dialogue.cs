using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    //you are given a starting point for the conversation. You scan the first line and the npc will say somoething (response) and give a link
    //the link will point to another point to another node, this node holds no response because 

    private static string filepath = "Assets/Resources/Dialogue/";
    string[] lines;


    void Start()
    {
        
        
    }

    // Will be passed a while name, such as Scene2Dialogue 
        public void Talk(string fileName)
    {
        //open csv and get all in a tree
        lines = System.IO.File.ReadAllText(filepath+fileName+".csv").Split('\n');
        ConvNode startNode = new ConvNode();
        buildTree(startNode, 1);

        Debug.Log(startNode.links[0].links[0].reply);
    }

    public void buildTree(ConvNode cNode, int currentLine)
    {
        int j = 0;
        int nextLine;
        //break each element into its own slot in array
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