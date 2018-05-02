using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class Cutscene1 : MonoBehaviour
{

    public Animator anim;


    
    public GameObject You;
    public GameObject Grandfather;
    public GameObject LordAbaddon;
    public GameObject Doogy;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public ExampleDialogueUI diagscript;

    int currentline;
    int temp = -1;
    public bool diagstarted = true;
    DialogueRunner dai = new DialogueRunner();

    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
        StartMainCharacterWalk("You");
        
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
                        cam1.enabled = false;
                        cam2.enabled = true;
                        
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
                            Doogy.SetActive(true);
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
                            cam2.enabled = false;
                            cam3.enabled = true;
                            StopTalking("Lord Abaddon");
                            StartTalking("You");
                            StartWalkingDoogy();
                            temp = currentline;
                        }
                        break;
                    case 14:
                        if (Input.GetMouseButtonDown(0))
                        {   

                            StopTalking("You");
                            EndSceneAnims();
                            temp = currentline;
                            diagscript.DialogueComplete();
                        }
                        break;

                    default:
                        break;

                }

            }
        }
    }

    public void SetDiagStart()
    {
        diagstarted = true;

        GameObject go = GameObject.Find("Dialogue");
        DialogueRunner test = (DialogueRunner)go.GetComponent(typeof(DialogueRunner));
        test.StartDialogue();
    }



    void StartTalking(string s)
    {
        GameObject gub = You;

        string choice = "Talk" + Random.Range(1, 8).ToString();
        if (s == "Lord Abaddon")
        {
            gub = LordAbaddon;
        }
        if (s == "Grandfather")
        {
            gub = Grandfather;
        }

        anim = gub.GetComponent<Animator>();
        anim.Play(choice, -1, 0f);

    }
    
    void StopTalking(string s)
    {
        GameObject gub2 = You;
        if (s == "Lord Abaddon")
        {
            gub2 = LordAbaddon;
        }
        if (s == "Grandfather")
        {
            gub2 = Grandfather;
        }
        anim = gub2.GetComponent<Animator>();
        anim.Play("Idle", -1, 0f);
    }

    void StartMainCharacterWalk(string s)
    {
        GameObject u = You;
        anim = u.GetComponent<Animator>();
        anim.Play("StartWalkFromHouse", -1, 0f);
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
        GameObject gObj = Doogy;
        anim = gObj.GetComponent<Animator>();
        if (gObj == Doogy)
        {
            anim.Play("Walk 3", -1, 0f);
        }
    }

    void EndSceneAnims()
    {
        GameObject gub = You;
        anim = gub.GetComponent<Animator>();
        anim.Play("StandQuarterTurnLeft");

        GameObject gub2 = Doogy;
        anim = gub2.GetComponent<Animator>();
        anim.Play("StandHalfTurnRight", -1, 0f);
        

        

    }



}