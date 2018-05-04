using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;
using UnityEngine.Playables;

public class Cutscene7 : MonoBehaviour
{


    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    public Camera cam5;

    public ExampleDialogueUI diagscript;
    public PlayableDirector timelinecontroller;

    //characters
    public GameObject You;
    public GameObject Doogy;
    public GameObject JasonMalas;
    public GameObject EnlilDecet;
    public GameObject JhovanRifiuti;

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
                        StartTalking("EnlilDecet");
                        temp = currentline;
                        break;
                    case 1:
                        if (Input.anyKey)
                        {

                            StopTalking("EnlilDecet");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 3:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 5:
                        if (Input.anyKey)
                        {
                            StopTalking("JasonMalas");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 6:
                        if (Input.anyKey)
                        {
                            cam1.enabled = false;
                            cam4.enabled = true;
                            Play();
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 9:
                        if (Input.anyKey)
                        {
                            
                            cam4.enabled = false;
                            cam5.enabled = true;
                            Play();
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 12:
                        if (Input.anyKey)
                        {
                            cam5.enabled = false;
                            cam1.enabled = true;
                            StopTalking("Doogy");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 14:
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 16:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 19:
                        if (Input.anyKey)
                        {
                            //StartWalking();
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 20:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 21:
                        if (Input.anyKey)
                        {
                            StopTalking("JasonMalas");
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

        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = Doogy;

        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = EnlilDecet;

        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = JhovanRifiuti;

        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = JasonMalas;

        anim = gub.GetComponent<Animator>();
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
        if (s == "EnlilDecet")
        {
            gub = EnlilDecet;
        }
        if (s == "JhovanRifiuti")
        {
            gub = JhovanRifiuti;
        }
        if (s == "JasonMalas")
        {
            gub = JasonMalas;
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
        if (s == "EnlilDecet")
        {
            gub2 = EnlilDecet;
        }
        if (s == "JhovanRifiuti")
        {
            gub2 = JhovanRifiuti;
        }
        if (s == "JasonMalas")
        {
            gub2 = JasonMalas;
        }

        anim = gub2.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
    }

    void StartWalking()
    {
        //GameObject gub = You;
        //GameObject gub2 = Doogy;

       // anim = gub.GetComponent<Animator>();
       // anim.Play("Walkfast", -1, 0f);

        //anim = gub2.GetComponent<Animator>();
        //anim.Play("Walkfast", -1, 0f);

    }

    public void StopWalking()
    {
       // GameObject gub4 = You;
        //GameObject gub5 = Doogy;
       // anim = gub4.GetComponent<Animator>();
       // anim.Play("Idle", -1, 0f);
       // anim = gub5.GetComponent<Animator>();
       // anim.Play("Idle", -1, 0f);
        //exitedTrig = false;
        //charactersStopped = true;
    }

    public void Play()
    {
        timelinecontroller.Play();
    }





}

