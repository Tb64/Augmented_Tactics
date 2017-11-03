using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private float healthPercent;
    private Transform mainCamera;
    private static Image[] playerBars;
    private static Image[] enemyBars;
    private TileMap map;
    private const string filename = "UI/HealthBarBase";
     
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<TileMap>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        /* Needs to start AFTER all actors loaded
        healthPercent = 100;//gameObject.GetComponentInParent<Actor>().GetHealthPercent(); later will fetch from actor
        enemyBars = new Image[Enemy.enemyNum];
        playerBars = new Image[PlayerControlled.playerNum];
        GameObject[] tempUIObjects = GameObject.FindGameObjectsWithTag("UIHealthBar");

        //all UI health bars are in tempUIObjects, assign each image component in those ojects as a healthbar
        Debug.Log(message: tempUIObjects.Length + " " + PlayerControlled.playerNum);
        if (PlayerControlled.playerNum != 0)
            for (int i = 0; i < tempUIObjects.Length; i++)
                playerBars[i] = tempUIObjects[i].GetComponent<Image>();
        Debug.Log(Enemy.enemyNum);
        if (Enemy.enemyNum != 0)
            for (int i = 0; i < Enemy.enemyNum; i++)
            {
                //instantiate a healthbar to enemy
                //Enemy.enemyList[i] = Instantiate(HealthBarBase, )
                GameObject healthBase = Instantiate(Resources.Load(filename)) as GameObject;
                Debug.Log(message: "is healthbase null?" + healthBase == null);
                healthBase.transform.parent = Enemy.enemyList[i].transform;
            }

        //enemyBars[i] = Enemy.enemyList[i].GetComponentInChildren<Image>();
        //will be a problem if we have multiple images. Can seach all components of type image then filter by tag
        */
    }

    public void updateHealth(float health)
    {
        if (gameObject.transform.childCount == 2)
        {
            gameObject.transform.GetChild(1).transform.localScale = new Vector3(health, 1f, 1f);
        }
    }
        

// Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera);
       
    }

    public static void UpdateHealth()
    {

        

        for (int i = 0; i < playerBars.Length; i++)
            playerBars[i].fillAmount = PlayerControlled.playerList[i].GetHealthPercent();
        for (int i = 0; i < enemyBars.Length; i++)
            ;//update the second child of the healthbar to transform it
    }

}

    
