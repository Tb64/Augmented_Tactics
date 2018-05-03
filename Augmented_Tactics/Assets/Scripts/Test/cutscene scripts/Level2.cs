using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour {
    public GameObject Cutscene2;
    public GameObject Cutscene2b;
    public GameObject Cutscene2c;
    public GameObject Level2TestPrefab;
    public int scene = 0;

    
    void Start () {
        
        
        
	}

    void Update()
    {
        if (GameObject.Find("Cutscene Controller2").GetComponent <Cutscene2>().sceneDone)
        {
            scene = 1;
        }
        if (GameObject.Find("Cutscene Controller2b").GetComponent<Cutscene2b>().sceneDone)
        {
            scene = 2;
        }
        if (GameObject.Find("Cutscene Controller2c").GetComponent<Cutscene2c>().sceneDone)
        {
            scene = 3;
        }


        if (scene == 1)
        {
            scene = 0;
            Cutscene2b.SetActive(true);
        }
        if(scene == 2)
        {
            scene = 0;
            Cutscene2b.SetActive(false);
            Cutscene2c.SetActive(true);
        }
        if(scene == 3)
        {
            Cutscene2c.SetActive(false);
            Level2TestPrefab.SetActive(true);
            
        }
    }
	
	
}
