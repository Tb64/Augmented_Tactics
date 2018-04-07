using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;

public class Cutscene1 : MonoBehaviour
{

    public Animator anim;
    int currentline;
    int temp = -1;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentline = ExampleDialogueUI.GetLineCount();
        if (temp != currentline)
        {

            //start
            switch (currentline)
            {
                case 0:
                    StartWalking("You");
                    StartTalking("Lord Abaddon");
                    temp = currentline;
                    break;
                case 2:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("Grandfather");
                        temp = currentline;
                    }
                    break;
                case 5:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Grandfather");
                        StartTalking("Lord Abaddon");
                        temp = currentline;
                    }
                    break;
                case 6:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("Grandfather");
                        temp = currentline;
                    }
                    break;
                case 9:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Grandfather");
                        StartTalking("Lord Abaddon");
                        temp = currentline;
                    }
                    break;
                case 10:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("Grandfather");
                        StartWalkingDoogy();
                        temp = currentline;
                    }
                    break;
                case 11:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Grandfather");
                        StartTalking("Lord Abaddon");
                        temp = currentline;
                    }
                    break;
                case 12:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("Lord Abaddon");
                        StartTalking("You");
                        temp = currentline;
                    }
                    break;
                case 14:
                    if (Input.GetMouseButtonDown(0))
                    {
                        StopTalking("You");
                        EndSceneAnims();

                        temp = currentline;
                    }
                    break;

                default:
                    break;

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