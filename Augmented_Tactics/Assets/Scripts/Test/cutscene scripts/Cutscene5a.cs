using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class Cutscene5a: MonoBehaviour
{

    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;

    //characters
    public GameObject You;
    public GameObject Doogy;
    public GameObject JhovanRifiuti;
    public GameObject FrederickDecet;
    public GameObject JohnCausion;
    public GameObject AdonaiBrevary;

    //objects
    public GameObject Rift;
    public GameObject DirecLight;

    public GameObject UndeadKnight;
    public GameObject UndeadKnight2;


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

                        StartTalking("JhovanRifiuti");
                        temp = currentline;
                        break;
                    case 1:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 2:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JohnCausion");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 4:
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
                    case 8:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 10:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 11:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 12:
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
                    case 17:
                        if (Input.GetMouseButtonDown(0))
                        {
                            cam1.enabled = false;
                            cam2.enabled = true;
                            AdonaiBrevary.SetActive(true);
                            StartTalking("FrederickDecet");
                            StopTalking("Doogy");
                            TurnAround("You");
                            TurnAround("Doogy");
                            TurnAround("JohnCausion");
                            TurnAround("JhovanRifiuti");
                            StartTalking("FrederickDecet");

                            temp = currentline;
                        }
                        break;
                    case 18:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 19:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 20:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 21:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("FrederickDecet");
                            StartWalking();
                            Wait();
                            temp = currentline;
                        }
                        break;
                    case 22:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;

                        }
                        break;
                    case 23:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 24:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 25:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 27:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 29:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 30:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 32:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 34:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            Rift.SetActive(true);
                            UndeadKnight.SetActive(true);
                            UndeadKnight2.SetActive(true);
                            temp = currentline;
                        }
                        break;
                    case 35:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 36:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 37:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 38:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                            DirecLight.SetActive(true);
                        }
                        break;
                    case 39:
                        
                        if (Input.GetMouseButtonDown(0))
                        {
                            DirecLight.SetActive(true);
                            Rift.SetActive(false);
                            DirecLight.SetActive(false);
                            SkellyAttack();
                            GetHit();
                            Wait();
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 40:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 42:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 43:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 44:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 46:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 48:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 49:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 50:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 51:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 52:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 53:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JohnCausion");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 54:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 56:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("JohnCausion");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 58:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("Doogy");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 60:
                        if (Input.GetMouseButtonDown(0))
                        {
                            StopTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                        
                    default:
                        break;

                }

            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(10);
    }

    void SkellyAttack()
    {
        GameObject skelly1 = UndeadKnight;
        GameObject skelly2 = UndeadKnight2;
        anim = skelly1.GetComponent<Animator>();
        anim2 = skelly2.GetComponent<Animator>();

        anim.Play("KnightTurn");
        anim2.Play("Knight2Turn");
    }

    void GetHit()
    {
        GameObject g = FrederickDecet;
        anim = g.GetComponent<Animator>();
        anim.Play("Hit");
    }

    void StartTalking(string s)
    {
        GameObject gub = You;

        string choice = "Talk" + Random.Range(2, 4).ToString();
        if (s == "JohnCausion")
        {
            gub = JohnCausion;
        }
        if (s == "AdonaiBrevary")
        {
            gub = AdonaiBrevary;
        }
        if (s == "FrederickDecet")
        {
            gub = FrederickDecet;
        }
        if (s == "JhovanRifiuti")
        {
            gub = JhovanRifiuti;
        }
        if (s == "Doogy")
        {
            gub = Doogy;
        }
        

        anim = gub.GetComponent<Animator>();
        anim.Play(choice, -1, 0f);

    }

    void StopTalking(string s)
    {
        string animchoice = "Idle";

        GameObject gub2 = You;
        if (s == "JohnCausion")
        {
            gub2 = JohnCausion;
        }
        if (s == "FrederickDecet")
        {
            gub2 = FrederickDecet;
        }
        if (s == "JhovanRifiuti")
        {
            gub2 = JhovanRifiuti;
        }
        if (s == "Doogy")
        {
            gub2 = Doogy;
        }
        if (s == "AdonaiBrevary")
        {
            gub2 = AdonaiBrevary;
        }
        if (currentline >= 18 && gub2 != FrederickDecet)
        {
            animchoice = "Armed-Idle phase";
        }
        anim = gub2.GetComponent<Animator>();
        anim.Play(animchoice, -1, 0f);

    }

    void TurnAround(string s)
    {
        
        GameObject a = You;
        string choice = "YouTurn";

        if (s == "JohnCausion")
        {
            a = JohnCausion;
            choice = "CausionTurn";
        }
        if (s == "JhovanRifiuti")
        {
            a = JhovanRifiuti;
            choice = "RifiutiTurn";
        }
        if (s == "Doogy")
        {
            a = Doogy;
            choice = "DoogyTurn";
        }

        anim = a.GetComponent<Animator>();
        anim.Play(choice, -1, 0f);
    }


    void StartWalking()
    {
        GameObject x = AdonaiBrevary;

        anim = x.GetComponent<Animator>();
        anim.Play("AdonaiWalk", -1, 0f);
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

        if (Doogy.name == "Doogy")
        {
            anim.Play("Idle 0", -1, 0f);
        }

        if (You.name == "You")
        {
            anim.Play("StandQuarterTurnLeft");

        }

    }



}