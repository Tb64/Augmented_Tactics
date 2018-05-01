using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class Cutscene6 : MonoBehaviour {


    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    public Camera cam5;
    public Camera cam6;
    public Camera cam7;
    public Camera cam8;

    //characters
    public GameObject You;
    public GameObject Doogy;
    public GameObject JhovanRifiuti;
    public GameObject EnlilDecet;
    public GameObject JasonMalas;
    public ExampleDialogueUI diagscript;


    int currentline;
    int temp = -1;
    public bool diagstarted = true;
    DialogueRunner dai = new DialogueRunner();

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
                        cam2.enabled = false;
                        cam1.enabled = true;
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
                            StartTalking("EnlilDecet");
                            temp = currentline;
                        }
                        break;
                    case 3:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("EnlilDecet");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 4:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("EnlilDecet");
                            temp = currentline;
                        }
                        break;
                    case 5:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("EnlilDecet");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 6:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 7:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 9:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
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
                    case 14:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 15:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 16:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 17:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam1.enabled = false;
                            cam3.enabled = true;
                            StopTalking("JhovanRifiuti");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 18:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 19:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam3.enabled = false;
                            cam1.enabled = true;
                            StopTalking("JhovanRifiuti");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 20:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 22:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 23:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("Doogy");
                            temp = currentline;

                        }
                        break;
                    case 25:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 26:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 27:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 28:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam1.enabled = false;
                            cam7.enabled = true;
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 30:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam7.enabled = false;
                            cam1.enabled = true;
                            StopTalking("JhovanRifiuti");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 31:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam1.enabled = false;
                            cam5.enabled = true;
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 32:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam5.enabled = false;
                            cam6.enabled = true;
                            StopTalking("You");
                            StartTalking("JasonMalas");
                            StartWalking();
                            temp = currentline;
                        }
                        break;
                    case 34:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = false;
                            cam8.enabled = true;
                            StopTalking("JasonMalas");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 35:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 36:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = true;
                            cam8.enabled = false;
                            StopTalking("Doogy");
                            StartTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 39:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = false;
                            cam8.enabled = true;
                            StopTalking("JasonMalas");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 42:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = true;
                            cam8.enabled = false;
                            StopTalking("You");
                            StartTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 44:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 45:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = false;
                            cam8.enabled = true;
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 46:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = true;
                            cam8.enabled = false;
                            StopTalking("You");
                            StartTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 49:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = false;
                            cam8.enabled = true;
                            StopTalking("JasonMalas");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 50:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("EnlilDecet");
                            temp = currentline;
                        }
                        break;
                    case 52:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("EnlilDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 53:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 54:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 55:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 56:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam6.enabled = true;
                            cam8.enabled = false;
                            StopTalking("Doogy");
                            StartTalking("JasonMalas");
                            temp = currentline;
                        }
                        break;
                    case 57:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JasonMalas");
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

        string choice = "Talk" + Random.Range(2, 4).ToString();
        
        if (s == "EnlilDecet")
        {
            gub = EnlilDecet;
        }
        if (s == "JhovanRifiuti")
        {
            gub = JhovanRifiuti;
        }
        if (s == "Doogy")
        {
            gub = Doogy;
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

        if (s == "EnlilDecet")
        {
            gub2 = EnlilDecet;
        }
        if (s == "JhovanRifiuti")
        {
            gub2 = JhovanRifiuti;
        }
        if (s == "Doogy")
        {
            gub2 = Doogy;
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
        GameObject gub = You;
        GameObject gub2 = JhovanRifiuti;
        GameObject gub3 = EnlilDecet;
        GameObject gub4 = Doogy;

        anim = gub.GetComponent<Animator>();
        anim.Play("GroupWalk", -1, 0f);

        anim = gub2.GetComponent<Animator>();
        anim.Play("GroupWalk", -1, 0f);

        anim = gub3.GetComponent<Animator>();
        anim.Play("GroupWalk", -1, 0f);

        anim = gub4.GetComponent<Animator>();
        anim.Play("GroupWalk", -1, 0f);

    }

    



}