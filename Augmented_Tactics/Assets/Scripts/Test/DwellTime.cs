using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellTime : MonoBehaviour {

    static public DwellTime instance;
    static private float dwellTime = 0f;
    
    void Awake()
    { 
        instance = this; 
    }

    IEnumerator AttackDwell()
    { //the coroutine that runs on our monobehaviour instance
        float dwell_time = dwellTime;

        yield return new WaitForSeconds(dwell_time);

        Debug.Log("Attack dwell finished");
        TurnBehaviour.ActorHasAttacked();
    }
    //=====
    public void funcTemplate()
    {

    }

    public void setDelay(float delay, GameObject temp)
    {
        StartCoroutine(wait(delay));
    }

    IEnumerator wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    //======
    static public void Attack(float input)
    {
        dwellTime = input;
        if(instance != null)
        {
            instance.StartCoroutine("AttackDwell"); //this will launch the coroutine on our instance
        }
        else
        {
            Debug.Log("Dwell ref is null");
        }
    }
}
