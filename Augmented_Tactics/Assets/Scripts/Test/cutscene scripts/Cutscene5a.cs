using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity.Example;
using Yarn.Unity;

public class Cutscene5a : MonoBehaviour
{

    public Animator anim;
    public Animator anim2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    public ExampleDialogueUI diagscript;

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
    public AudioClip tune;
    public AudioClip tune2;
    public AudioSource musicsource;
    public GameObject UndeadKnight;
    public GameObject UndeadKnight2;
    public bool Donetalking = false;


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
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 2:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 4:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 5:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 6:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 8:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 10:
                        if (Input.anyKey)
                        {
                            cam3.enabled = true;
                            cam1.enabled = false;
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 11:
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 12:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 14:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 15:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 17:
                        if (Input.anyKey)
                        {
                            StartTalking("FrederickDecet");
                            StopTalking("Doogy");
                            TurnAround("JhovanRifiuti");
                            musicsource.Stop();
                            temp = currentline;
                        }
                        break;
                    case 18:
                        if (Input.anyKey)
                        {
                            musicsource.clip = tune;
                            musicsource.Play();
                            FrederickDecet.SetActive(true);
                            cam3.enabled = false;
                            cam2.enabled = true;
                            TurnAround("You");
                            TurnAround("Doogy");
                            TurnAround("JohnCausion");
                            StopTalking("FrederickDecet");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 19:
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 20:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 21:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("FrederickDecet");
                            AdonaiBrevary.SetActive(true);
                            StartWalking();
                            Wait();
                            temp = currentline;
                        }
                        break;
                    case 22:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;

                        }
                        break;
                    case 23:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 24:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 25:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 27:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 29:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 30:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 32:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 34:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
                            Rift.SetActive(true);
                            UndeadKnight.SetActive(true);
                            UndeadKnight2.SetActive(true);
                            temp = currentline;
                        }
                        break;
                    case 35:
                        if (Input.anyKey)
                        {
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 36:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                        }
                        break;
                    case 37:
                        if (Input.anyKey)
                        {
                            StopTalking("FrederickDecet");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 38:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("FrederickDecet");
                            temp = currentline;
                            DirecLight.SetActive(true);
                        }
                        break;
                    case 39:
                        if (Input.anyKey)
                        {
                            musicsource.Stop();
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
                        if (Input.anyKey)
                        {
                            musicsource.clip = tune2;
                            musicsource.Play();
                            TurntoAdonai();
                            StopTalking("You");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 42:
                        if (Input.anyKey)
                        {
                            PlayerTurntoAdonai();
                            StopTalking("AdonaiBrevary");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 43:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("You");
                            temp = currentline;
                        }
                        break;
                    case 44:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 46:
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 48:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 49:
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 50:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 51:
                        if (Input.anyKey)
                        {
                            StopTalking("You");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 52:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 53:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("JhovanRifiuti");
                            temp = currentline;
                        }
                        break;
                    case 54:
                        if (Input.anyKey)
                        {
                            StopTalking("JhovanRifiuti");
                            StartTalking("JohnCausion");
                            temp = currentline;
                        }
                        break;
                    case 56:
                        if (Input.anyKey)
                        {
                            StopTalking("JohnCausion");
                            StartTalking("Doogy");
                            temp = currentline;
                        }
                        break;
                    case 58:
                        if (Input.anyKey)
                        {
                            StopTalking("Doogy");
                            StartTalking("AdonaiBrevary");
                            temp = currentline;
                        }
                        break;
                    case 60:
                        if (Input.anyKey)
                        {
                            StopTalking("AdonaiBrevary");
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
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(10);
    }
    void TurntoAdonai()
    {
        GameObject gub2 = Doogy;
        GameObject gub3 = JhovanRifiuti;
        GameObject gub4 = JohnCausion;


        anim = gub2.GetComponent<Animator>();
        anim.Play("lastturn");
        anim = gub3.GetComponent<Animator>();
        anim.Play("lastturn");
        anim = gub4.GetComponent<Animator>();
        anim.Play("lastturn");

    }
    void PlayerTurntoAdonai()
    {
        GameObject gub = You;

        anim = gub.GetComponent<Animator>();
        anim.Play("lastturn");

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