using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;

public class Cutscene2 : MonoBehaviour
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
                    StartWalking("Frederick Decet");
                    StartWalking("John Causion");
                    StartTalking("John Causion");
                    temp = currentline;
                    break;

                case 2:
                    StopTalking("John Causion");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("Frederick Decet");
                        temp = currentline;
                    }
                    break;
                case 4:
                    StopTalking("Frederick Decet");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("John Causion");
                        temp = currentline;
                    }
                    break;
                case 6:
                    StopTalking("John Causion");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("Frederick Decet");
                        temp = currentline;
                    }
                    break;
                case 8:
                    StopTalking("Frederick Decet");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("John Causion");
                        temp = currentline;
                    }
                    break;
                case 10:
                    StopTalking("John Causion");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("Frederick Decet");
                        temp = currentline;
                    }
                    break;

                case 11:
                    StopTalking("Frederick Decet");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("John Causion");
                        temp = currentline;
                    }
                    break;
                case 12:
                    StopTalking("John Causion");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("Frederick Decet");
                        temp = currentline;
                    }
                    break;
                case 13:
                    StopTalking("Frederick Decet");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("John Causion");
                        temp = currentline;
                    }
                    break;
                case 14:
                    StopTalking("John Causion");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("Frederick Decet");
                        temp = currentline;
                    }
                    break;
                case 15:
                    StopTalking("Frederick Decet");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("John Causion");
                        temp = currentline;
                    }
                    break;
                case 16:
                    StopTalking("John Causion");
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartTalking("Frederick Decet");
                        temp = currentline;
                    }
                    break;
                case 17:
                    StopTalking("John Causion");
                    
                    break;

                default:
                    break;

            }

        }
    }
    

    void StartTalking(string s)
    {
        anim.ResetTrigger("LinesDone");
        string choice = "Talk" + Random.Range(2, 3).ToString();
        if (gameObject.name == s)
        {
            anim.Play(choice, -1, 0f);
        }
    }
    void StopTalking(string s)
    {
        if (gameObject.name == s)
        {
            anim.Play("Idle", -1, 0f);
        }


    }

    void StartWalking(string s)
    {
        if (gameObject.name == s)
        {
            anim.Play("Walk", -1, 0f);
        }
    }
    

}