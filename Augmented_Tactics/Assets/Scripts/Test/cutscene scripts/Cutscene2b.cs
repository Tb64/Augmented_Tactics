using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;
using UnityEngine.Playables;

public class Cutscene2b : MonoBehaviour {



    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public bool sceneDone;
    public bool wasCalled = false;

    public ExampleDialogueUI diagscript;
    public PlayableDirector timelinecontroller;

    //characters
    public GameObject Herald;
    public GameObject Eery;
    public GameObject FrederickDecet;

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
                        StartTalking("Herald");
                        temp = currentline;
                        break;
                    case 1:
                        if (Input.anyKey)
                        {

                            StopTalking("Herald");
                            StartTalking("Eery");
                            temp = currentline;
                        }
                        break;
                    case 2:
                        if (Input.anyKey)
                        {
                            cam2.enabled = false;
                            cam1.enabled = true;
                            StopTalking("Eery");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 3:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("Eery");
                            temp = currentline;
                        }
                        break;
                    case 4:
                        if (Input.anyKey)
                        {
                            
                            StopTalking("Eery");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 5:
                        if (Input.anyKey)
                        {

                            
                            StopTalking("FrederickDecet");
                            StartTalking("Eery");
                            temp = currentline;
                            
                        }
                        break;
                    case 6:
                        if (Input.anyKey)
                        {
                            cam1.enabled = false;
                            cam3.enabled = true;
                            Play();
                            StopTalking("Eery");
                            TurnAround();
                            temp = currentline;
                        }
                        break;
                    case 7:
                        if (Input.anyKey)
                        {
                            
                        }
                        break;
                    case 8:
                        if (Input.anyKey)
                        {
                            diagscript.DialogueComplete();
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

        GameObject gub = FrederickDecet;

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
        GameObject gub = Herald;

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

        GameObject gub2 = Herald;

        
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

    
    public void TurnAround()
    {
        GameObject gub = Herald;

        anim = gub.GetComponent<Animator>();
        anim.Play("turn", -1, 0f);

        gub = Eery;

        anim = gub.GetComponent<Animator>();
        anim.Play("turn", -1, 0f);

        gub = FrederickDecet;

        anim = gub.GetComponent<Animator>();
        anim.Play("Walk", -1, 0f);
    }

    public void Play()
    {
        timelinecontroller.Play();
    }
    


}
