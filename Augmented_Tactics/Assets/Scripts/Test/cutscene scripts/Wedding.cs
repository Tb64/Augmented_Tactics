using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;
using UnityEngine.Playables;

public class Wedding : MonoBehaviour {

    public Animator anim;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;

    public ExampleDialogueUI diagscript;
    public PlayableDirector timelinecontroller;

    //characters
    public GameObject LordAbaddon;
    public GameObject Eery;
    public GameObject FrederickDecet;
    public GameObject Herald;
    public GameObject Hunter;
    public GameObject Aggressive;
    public GameObject HumanMale5;

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
                        StartTalking("Herald");
                        temp = currentline;
                        break;
                    case 1:
                        if (Input.anyKey)
                        {
                            cam1.enabled = false;
                            cam2.enabled = true;
                            temp = currentline;
                        }
                        break;
                    case 2:
                        if (Input.anyKey)
                        {
                            cam2.enabled = false;
                            cam3.enabled = true;
                        }
                        break;
                    case 3:
                        if (Input.anyKey)
                        {
                            Play();
                            TrigDudeStart();
                            
                        }
                        break;
                    

                    default:
                        break;

                }

            }
        }
    }
    public void EndScene()
    {
        GameObject obj = GameObject.Find("Switcher");
        if (obj != null)
        {
            obj.GetComponent<Switcher>().NextObjectLoad();
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

        gub = Aggressive;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

        gub = HumanMale5;
        anim = gub.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);

    }

    void StartTalking(string s)
    {
        GameObject gub = Herald;

        anim = gub.GetComponent<Animator>();
        anim.Play("Talk2", -1, 0f);

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

    public void TrigDudeStart()
    {
        GameObject gub = HumanMale5;
        anim = gub.GetComponent<Animator>();
        anim.Play("humanturn", -1, 0f);
    }
    public void Play()
    {
        timelinecontroller.Play();
    }



}
