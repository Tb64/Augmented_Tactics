using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private static FloatingDamage popupText;
    private static GameObject canvas;
    private static Transform mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    public static void initializeText()
    {
        canvas = GameObject.Find("DamageNumber");
       
        if (popupText == null)
        {
            Debug.Log("Loading");
            popupText = Resources.Load<FloatingDamage>("DamageText");
        }
    }

    public static void createFloatingText(string text, Transform location, Color color)
    {

        FloatingDamage instance = Instantiate(popupText);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(location.position + new Vector3(0,2,0));

        instance.transform.SetParent(canvas.transform, false);
        //instance.transform.position = screenPosition;
        instance.transform.position = location.transform.position + new Vector3(0, 2, 0);
        
        instance.setText(text, color);
        //instance.transform.LookAt(mainCamera);
    }

   
}
