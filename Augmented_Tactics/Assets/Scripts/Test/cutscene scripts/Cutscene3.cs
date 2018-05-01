using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class Cutscene3 : MonoBehaviour {


    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;

    public ExampleDialogueUI diagscript;

    //characters
    public GameObject You;
    public GameObject Doogy;

    int currentline;
    int temp = -1;
    public bool diagstarted = true;
    public bool walking = false;
    public bool charactersStopped = false;

    // Use this for initialization
    void Start()
    {
        Idle();
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
                        StartTalking("You");
                        temp = currentline;
                        break;
                    case 1:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 2:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 3:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 5:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 6:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 7:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam1.enabled = false;
                            cam2.enabled = true;
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 8:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 10:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartWalking();
                            cam2.enabled = false;
                            cam3.enabled = true;
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 11:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 12:
                        if (Input.GetMouseButtonDown(0))
                        {
                            
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 13:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 14:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 16:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 17:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 18:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 19:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 20:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopWalking();
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                            diagscript.DialogueComplete();
                        }
                        break;
                    case 21:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            temp = currentline;
                        }
                        break;


                    default:
                        break;

                }

            }
        }
    }

    public void Idle()
    {
        GameObject gub = You;
        GameObject gub2 = Doogy;

        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        anim = gub2.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
    }

    void StartTalking(string s)
    {
        GameObject gub = You;

        string choice = "Talk" + Random.Range(2, 3).ToString();
        
        if (s == "Doogy")
        {
            gub = Doogy;
        }


        anim = gub.GetComponent<Animator>();
        anim.Play(choice, -1, 0f);

    }

    void StopTalking(string s)
    {

        GameObject gub2 = You;

        if (s == "Doogy")
        {
            gub2 = Doogy;
        }

        anim = gub2.GetComponent<Animator>();

        if (walking)
        {
            anim.Play("WalkArms", -1, 0f);
        }
        else
        {
            anim.Play("IdleArms", -1, 0f);
        }
    }

    void StartWalking()
    {
        GameObject gub = You;
        GameObject gub2 = Doogy;

        anim = gub.GetComponent<Animator>();
        anim.Play("Walkfast", -1, 0f);

        anim = gub2.GetComponent<Animator>();
        anim.Play("Walkfast", -1, 0f);

    }

    public void StopWalking()
    {
        GameObject gub4 = You;
        GameObject gub5 = Doogy;
        anim = gub4.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
        anim = gub5.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
        //exitedTrig = false;
        //charactersStopped = true;
    }





}

