using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    // Use this for initialization
    private ConvNode testnode1;
    private ConvNode testnode2;
    Queue<string> sentences;
    private string filepath;

    public void Talk()
    {
        

    }
}

public class ConvNode{
    public string prompt;
    public string[] replies;
}
