using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;

public class Cutscene1 : MonoBehaviour {

    public Animator anim;
    int currentline;
    int temp = -1;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        currentline = ExampleDialogueUI.GetLineCount();
        if (temp != currentline)
        {

            //start
            if (currentline == 0)
            {
                StartWalking("You");
                StartTalking("Lord Abaddon");
                temp = currentline;
            }

            //line 2
            if (currentline == 2)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("Grandfather");
                        temp = currentline;
                    }
                    i = 1;
                }
            }

            // line 5
            if (currentline == 5)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Grandfather");
                        StartTalking("Lord Abaddon");
                        temp = currentline;
                    }
                    i = 1;
                }
            }

            // line 6
            if (currentline == 6)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("Grandfather");
                        temp = currentline;
                    }
                    i = 1;
                }
            }

            //line 9
            if (currentline == 9)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Grandfather");
                        StartTalking("Lord Abaddon");
                        temp = currentline;
                    }
                    i = 1;
                }
            }

            //line 10
            if (currentline == 10)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("Grandfather");
                        StartWalkingDoogy();
                        temp = currentline;
                    }
                    i = 1;
                }
            }

            //line 11
            if (currentline == 11)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Grandfather");
                        StartTalking("Lord Abaddon");
                        temp = currentline;
                    }
                    i = 1;
                }
            }

            //line 12
            if (currentline == 12)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("You");

                        temp = currentline;
                    }
                    i = 1;
                }
            }

            



            //line 14
            if (currentline == 14)
            {
                int i = 0;
                while (i == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("You");
                        EndSceneAnims();

                        temp = currentline;
                    }
                    i = 1;
                }
            }

        }
    }

    void StartTalking(string s)
    {
        string theguy = s;
        string choice = "Talk" + Random.Range(1, 8).ToString();
        
        if (gameObject.name == s)
        {
            anim.Play(choice, -1, 0f);
        }
    }
    void StopTalking(string s)
    {
        string theguy = s;
        if (gameObject.name == s)
        {
            anim.Play("Idle", -1, 0f);
        }
    }

    void StartWalking(string s)
    {
        string theguy = s;
        if (gameObject.name == s)
        {
            anim.Play("Walk", -1, 0f);
        }
    }

    void StartWalkingDoogy()
    {
        if (gameObject.name == "Doogy")
        {
            anim.Play("Walk 3", -1, 0f);
        }
    }

    void EndSceneAnims()
    {
        if (gameObject.name == "Doogy")
        {
            anim.Play("Idle 0", -1, 0f);
        }

        if (gameObject.name == "You")
        {
            anim.Play("StandQuarterTurnLeft");

        }

    }



}
