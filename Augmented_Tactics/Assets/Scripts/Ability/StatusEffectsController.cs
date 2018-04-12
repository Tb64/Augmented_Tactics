using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffectsController : MonoBehaviour
{
    private static bool instantiated = false;
    public static List<Enemy> enemyList;
    public static Actor[] playerList;
    public static List<StatusEffects> allEffects;

    public static void AddEffect(StatusEffects status)
    {
        if (!instantiated)
        {
            enemyList = EnemyController.enemyList;
            playerList = EnemyController.userTeam;
            allEffects = new List<StatusEffects>();
            instantiated = true;
        }

        allEffects.Add(status);
    }

    public static void RemoveEffect(StatusEffects status)
    {
        allEffects.Remove(status);
        status.duration = 0;
    }

    public static bool Effected(Actor actor)
    {
        foreach(StatusEffects status in allEffects)
        {
            if(status.effectedPlayer == actor)
            {
                return true;
            }
        }
        return false;
    }
}