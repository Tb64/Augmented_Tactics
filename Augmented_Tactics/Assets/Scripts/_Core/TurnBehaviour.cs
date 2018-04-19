using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBehaviour : MonoBehaviour
{

    /******************
     *  Declarations
     ******************/

    #region Declarations

    // For Handling Global Game/Battle Events
    public delegate void GameEventHandler();
    public static event GameEventHandler OnGameStart;
    public static event GameEventHandler OnNewSelectedUnit;

    // For Handling Actor Events
    public delegate void ActorEventHandler();
    public static event ActorEventHandler OnActorSpawn;
    public static event ActorEventHandler OnActorMoved;
    public static event ActorEventHandler OnActorAttacked;
    public static event ActorEventHandler OnActorBeginsAttacking;
    public static event ActorEventHandler OnActorStartsMove;
    public static event ActorEventHandler OnActorFinishedMove;

    //for Handling Player Events
    public delegate void PlayerEventHandler();
    public static event PlayerEventHandler OnPlayerSpawn;
    public static event PlayerEventHandler OnPlayerJustMoved;
    public static event PlayerEventHandler OnPlayerAttack;
    public static event PlayerEventHandler OnUnitDestroy;
    public static event PlayerEventHandler OnPlayerStartMove;
    public static event PlayerEventHandler OnPlayerBeginsAttack;
    public static event PlayerEventHandler OnPlayerConfirmingAttack;

    //for Handling Turn Based Events
    public delegate void TurnEventHandler();
    public delegate void TurnEventHandlerBOO(bool turn);
    public static event TurnEventHandler OnTurnStart;
    public static event TurnEventHandlerBOO OnTurnEnd;
    public static event TurnEventHandler OnPlayerTurnStart;
    public static event TurnEventHandler OnPlayerTurnEnd;
    public static event TurnEventHandler OnEnemyTurnStart;
    public static event TurnEventHandler OnEnemyTurnEnd;

    //for Handling Enemy Events
    public delegate void EnemyEventHandler();
    public static event EnemyEventHandler OnEnemyUnitSpawn;
    public static event EnemyEventHandler OnEnemyUnitMoved;
    public static event EnemyEventHandler OnEnemyUnitAttack;
    public static event EnemyEventHandler OnEnemyUnitDestroy;
    public static event EnemyEventHandler OnEnemyBeginsMoving;
    public static event EnemyEventHandler OnEnemyOutOfMoves;
    public static event PlayerEventHandler OnEnemyBeginsAttacking;

    //for checking whose turn it is and total number of turns played
    private static bool isPlayerTurn;
    private static int numberOfTurns = 0;

    //number of players
    private static int numberOfActors;
    private static int numberOfEnemies;
    private static bool firstRun = true;

    #endregion

    public static void NextTurnEventTrigger(bool isPlayerTurn)
    {
        TurnStart();

        if (isPlayerTurn)
            PTurnStart();
        else
            ETurnStart();

        numberOfTurns++;
    }

    public static void Initialize()
    {
        firstRun = true;
        numberOfTurns = 0;
    }

    public static void Initialize(bool playerTurn)
    {
        isPlayerTurn = playerTurn;
        Initialize();
    }

    //public virtual void FixedUpdate()
    //{
    //    if (firstRun)
    //    {
    //        TurnStart();


    //        if (isPlayerTurn)
    //            PlayerTurnStart();
    //        else
    //            EnemyTurnStart();

    //        firstRun = false;
    //    }

    //}
    //public virtual void PlayerTurnStart()
    //{
    //    // Debug.Log("PlayerTurnStart");
    //}

    #region EVENT CALLERS

    //GAME EVENT CALLERS
    //calls Game Start Event
    public static void GStart()
    {
        if (OnGameStart != null)
            OnGameStart();
    }

    public static void NewSelectedUnit()
    {
        if (OnNewSelectedUnit != null)
            OnNewSelectedUnit();
    }

    //Called when a Move starts
    public static void ActorBeginsMoving()
    {
        if (!isPlayerTurn)
        {
            EnemyBeginsMoving();
        }
        else
        {
            PlayerBeginsMoving();
        }
        if (OnActorStartsMove != null)
            OnActorStartsMove();
    }

    //Called when a Move has finished
    public static void ActorHasJustMoved()
    {
        if (OnActorFinishedMove != null)
        {
            OnActorFinishedMove();
            if (!isPlayerTurn)
            {
                EnemyHasJustMoved();
            }
            else
            {
                PlayerHasJustMoved();
            }
        }
    }

    /// <summary>
    /// An event that happens when and actor has finished attacking
    /// </summary>
    public static void ActorHasAttacked()
    {
        if (!isPlayerTurn)
        {
            EnemyHasJustAttacked();
        }
        else
        {
            PlayerHasJustAttacked();
        }
        if (OnActorAttacked != null)
            OnActorAttacked();
    }

    public static void ActorBeginsAttacking()
    {
        if (!isPlayerTurn)
        {
            EnemyBeginsAttacking();
        }
        else
        {
            PlayerBeginsAttacking();
        }
        if (OnActorBeginsAttacking != null)
            OnActorBeginsAttacking();
    }

    //**************************
    //PLAYER EVENT CALLERS
    //calls New Player Added Event
    public static void NewPlayerAdded()
    {
        Debug.Log("New Player added");
        if (OnPlayerSpawn != null)
            OnPlayerSpawn();
    }
    //calls Player Begins Moving Event
    public static void PlayerBeginsMoving()
    {
        if (OnPlayerStartMove != null)
            OnPlayerStartMove();
    }
    //calls Player Moved Event
    public static void PlayerHasJustMoved()
    {
        if (OnPlayerJustMoved != null)
            OnPlayerJustMoved();
    }
    //calls Player Attacked Event
    public static void PlayerHasJustAttacked()
    {
        if (OnPlayerAttack != null)
            OnPlayerAttack();
    }
    //calls Player Destroyed Event
    public static void PlayerHasJustBeenDestroyed()
    {
        if (OnUnitDestroy != null)
            OnUnitDestroy();
    }

    /// <summary>
    /// calls Player Begins Attacking Event
    /// </summary>
    public static void PlayerBeginsAttacking()
    {
        if (OnPlayerBeginsAttack != null)
            OnPlayerBeginsAttack();
    }
    //calls Player Attacked Event
    public static void PlayerIsConfirmingAttack()
    {
        if (OnPlayerConfirmingAttack != null)
            OnPlayerConfirmingAttack();
    }

    //*****************************
    //TURN EVENT CALLERS

    //calls Turn Start Event
    public static void TurnStart()
    {
        if (OnTurnStart != null)
            OnTurnStart();
    }
    public static void TurnEnd(bool turn)
    {
        bool PT = turn;
        //PT = !PT;
        if (OnTurnEnd != null)
            OnTurnEnd(PT);
    }

    //calls Player Turn Start Event
    public static void PTurnStart()
    {
        if (OnPlayerTurnStart != null)
            OnPlayerTurnStart();
    }
    //calls Player Turn End Event
    public static void PTurnEnd()
    {
        if (OnPlayerTurnEnd != null)
            OnPlayerTurnEnd();
    }
    //calls Enemy Turn Start Event
    public static void ETurnStart()
    {
        if (OnEnemyTurnStart != null)
            OnEnemyTurnStart();
    }
    //calls Enemy Turn End Event
    public static void ETurnEnd()
    {
        if (OnEnemyTurnEnd != null)
            OnEnemyTurnEnd();
    }

    //*****************************
    //ENEMY EVENT CALLERS


    /// <summary>
    /// calls New Enemy Added Event
    /// </summary>
    public static void NewEnemyAdded()
    {
        if (OnEnemyUnitSpawn != null)
            OnEnemyUnitSpawn();
    }
    /// <summary>
    /// calls Enemy Moved Event
    /// </summary>
    //calls Enemy Begins Moving Event
    public static void EnemyBeginsMoving()
    {
        if (OnEnemyBeginsMoving != null)
            OnEnemyBeginsMoving();
    }
    //calls Enemy Moved Event
    public static void EnemyHasJustMoved()
    {
        if (OnEnemyUnitMoved != null)
            OnEnemyUnitMoved();
    }
    /// <summary>
    /// calls Enemy Attacked Event
    /// </summary>
    public static void EnemyHasJustAttacked()
    {
        if (OnEnemyUnitAttack != null)
            OnEnemyUnitAttack();
    }
    /// <summary>
    /// calls Enemy Destroyed Event
    /// </summary>
    public static void EnemyHasJustBeenDestroyed()
    {
        if (OnEnemyUnitDestroy != null)
            OnEnemyUnitDestroy();
    }
    //calls Enemy Begins Moving Event
    public static void EnemyBeginsAttacking()
    {
        if (OnEnemyBeginsAttacking != null)
            OnEnemyBeginsAttacking();
    }
    /// <summary>
    /// enemy done moving trigger next enemy
    /// </summary>
    public static void EnemyTurnFinished()
    {
        if (OnEnemyOutOfMoves != null)
            OnEnemyOutOfMoves();
    }



    #endregion

    /// Toggles isPlayerTurn and triggers newTurn event for actors.
    public void TurnEndActions()
    {
        //firstRun = true;
        //numberOfTurns++;
    }

    /// Sets the turn, if player's turn set true.


    #region SetGet
    /// Returns the current number of turns passed
    public static int GetTurnNumber()
    {
        return numberOfTurns;
    }

    /// Returns true if it is the player's turn
    public static bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    #endregion
}
