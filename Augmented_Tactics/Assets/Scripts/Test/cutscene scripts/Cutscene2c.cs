using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;
using UnityEngine.Playables;

public class Cutscene2c : MonoBehaviour {

    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;

    public ExampleDialogueUI diagscript;

    //characters
    public GameObject LordAbaddon;
    public GameObject Eery;
    public GameObject FrederickDecet;
    public GameObject Herald;
    public GameObject Hunter;

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
                        Idle();
                        StartTalking("LordAbaddon");
                        temp = currentline;
                        break;
                    case 1:
                        if (Input.anyKey)
                        {
                            StopTalking("LordAbaddon");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 2:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("LordAbaddon");
                            temp = currentline;
                        }
                        break;
                    case 4:
                        if (Input.anyKey)
                        {
                            StopTalking("LordAbaddon");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 5:
                        if (Input.anyKey)
                        {
                            cam1.enabled = false;
                            cam2.enabled = true;
                            StopTalking("FrederickDecet");
                            StartTalking("LordAbaddon");
                            temp = currentline;
                        }
                        break;
                    case 9:
                        if (Input.anyKey)
                        {
                            StopTalking("LordAbaddon");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 10:
                        if (Input.anyKey)
                        {
                            cam2.enabled = false;
                            cam3.enabled = true;
                            StopTalking("FrederickDecet");
                            StartTalking("LordAbaddon");
                            temp = currentline;
                        }
                        break;
                    case 11:
                        if (Input.anyKey)
                        {
                            StopTalking("LordAbaddon");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 12:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            diagscript.DialogueComplete();
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
        GameObject gub = LordAbaddon;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = Hunter;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = FrederickDecet;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = Herald;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = Eery;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

    }

    void StartTalking(string s)
    {
        GameObject gub = LordAbaddon;

        string choice = "Talk" + Random.Range(2, 3).ToString();


        if (s == "FrederickDecet")
        {
            gub = FrederickDecet;
        }
        if (s == "Eery")
        {
            gub = Eery;
        }


        anim = gub.GetComponent<Animator>();
        anim.Play(choice, -1, 0f);

    }

    void StopTalking(string s)
    {

        GameObject gub2 = LordAbaddon;


        if (s == "FrederickDecet")
        {
            gub2 = FrederickDecet;
        }
        if (s == "Eery")
        {
            gub2 = Eery;
        }

        anim = gub2.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
    }






}
