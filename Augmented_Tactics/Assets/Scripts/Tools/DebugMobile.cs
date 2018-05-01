using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMobile : MonoBehaviour {

    static List<string> logs;

    public static Text logDisplay;

    public static void Log(string input)
    {
        Debug.Log(input);
        GameObject gObj = GameObject.FindGameObjectWithTag("MobileDebug");

        if (gObj == null)
            return;
        if(logDisplay == null)
            logDisplay = gObj.GetComponent<Text>();

        if (logs == null)
            logs = new List<string>();

        logs.Add(Time.time.ToString("000.00") + "| " + input);

        if (logs.Count > 20)
            logs.RemoveAt(0);

        logDisplay.text = "";
        foreach (string line in logs)
        {
            logDisplay.text = line + "\n" + logDisplay.text;
        }
    }
}
