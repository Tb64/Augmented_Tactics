using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class Cutscene2 : MonoBehaviour
{
    public bool exitedTrig = false;
    public bool charactersStopped = false;
    public bool sceneDone = false;
    
    public Animator anim;
    int currentline;
    int temp = -1;
    public bool diagstarted = true;

    public GameObject FrederickDecet;
    public GameObject JohnCausion;
    public ExampleDialogueUI diagscript;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (diagstarted)
        {
            currentline = ExampleDialogueUI.GetLineCount();
            if (temp != currentline)
            {

                //start
                switch (currentline)
                {
                    case 0:
                        StartWalking("FrederickDecet");
                        StartWalking("JohnCausion");
                        StartTalking("JohnCausion");
                        temp = currentline;
                        break;
                    case 2:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 4:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 6:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 8:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 10:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 11:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 12:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 13:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 14:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 15:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            Debug.Log("END");
                            temp = currentline;
                            diagscript.DialogueComplete();
                            sceneDone = true;
                        }
                        break;
                    
                    default:
                        break;

                }

            }
        }
        


    }


    void StartTalking(string s)
    {
        GameObject gub = JohnCausion;

        string choice = "Talk" + Random.Range(2, 3).ToString();
        
        if (s == "FrederickDecet")
        {
            gub = FrederickDecet;
        }

        anim = gub.GetComponent<Animator>();
        anim.Play(choice, -1, 0f);


    }
    void StopTalking(string s)
    {
        GameObject gub2 = JohnCausion;
        
        if (s == "FrederickDecet")
        {
            gub2 = FrederickDecet;
        }
        anim = gub2.GetComponent<Animator>();

        if (charactersStopped)
        {
            anim.Play("Idle", -1, 0f);
        }
        else
        {
            anim.Play("WalkArms", -1, 0f);
        }
    }

    void StartWalking(string s)
    {
        GameObject gub3 = JohnCausion;

        if (s == "FrederickDecet")
        {
            gub3 = FrederickDecet;
        }
        anim = gub3.GetComponent<Animator>();
        anim.Play("Walk", -1, 0f);
    }
    public void StopWalking()
    {
        GameObject gub4 = JohnCausion;
        GameObject gub5 = FrederickDecet;
        anim = gub4.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
        anim = gub5.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
        exitedTrig = false;
        charactersStopped = true;
    }
    

}