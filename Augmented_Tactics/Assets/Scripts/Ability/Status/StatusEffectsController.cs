using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffectsController : MonoBehaviour
{
    public void Start()
    {
        instantiated = false;
    }
    private static bool instantiated;
    public static List<Enemy> enemyList;
    public static Actor[] playerList;
    public static List<StatusEffects> allEffects;
    public static List<Actor[]> bonded;

    public static void AddEffect(StatusEffects status)
    {
        if (!instantiated)
        {
            enemyList = EnemyController.enemyList;
            playerList = EnemyController.userTeam;
            allEffects = new List<StatusEffects>();
            bonded = new List<Actor[]>();
            instantiated = true;
        }
        if(!SameEffect(status))
            allEffects.Add(status);
    }

    public static bool SameEffect(StatusEffects stat)
    {
        foreach(StatusEffects status in allEffects)
        {
            if (status.getName() == status.getName() && ((stat.effectedPlayer == status.effectedPlayer) || (stat.effectedObject != null && status.effectedObject != null && stat.effectedObject == status.effectedObject)
            {
                return true;
            }
        }
        return false;
    }

    public static void RemoveEffect(StatusEffects status)
    {
        allEffects.Remove(status);
        status.duration = 0;
    }

    public static List<StatusEffects> GetEffects(Actor actor)
    {
        List<StatusEffects> statuses = new List<StatusEffects>();
        foreach(StatusEffects status in allEffects)
        {
            if(status.effectedPlayer == actor)
            {
                statuses.Add(status);
            }
        }
        if(statuses.Count == 0)
            return null;
        else
            return statuses;
    }
}
