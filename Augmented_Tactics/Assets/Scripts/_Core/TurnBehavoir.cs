using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBehavoir : MonoBehaviour {

    private static bool isPlayerTurn;
    private static int numberOfTurns;

    private static bool firstRun = true;

    public TurnBehavoir()
    {
        Initialize();
    }

    public TurnBehavoir(bool playerTurn)
    {
        Initialize(playerTurn);
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

    public virtual void FixedUpdate()
    {
        if(firstRun)
        {
            Debug.Log("FixedUpdate");
            TurnStart();
            if (isPlayerTurn)
                PlayerTurnStart();
            else
                EnemyTurnStart();
            firstRun = false;
        }
    }

    /// <summary>
    /// Runs on the start of everyone's Turn.  Runs once per turn. Runs concurrent with PlayerTurnStart/EnemyTurnStart.
    /// </summary>
    public virtual void TurnStart()
    {

    }

    /// <summary>
    /// Runs on the start of Player's Turn.  Runs once per turn.
    /// </summary>
    public virtual void PlayerTurnStart()
    {
        Debug.Log("PlayerTurnStart");
    }

    /// <summary>
    /// Runs on the start of Enemy's Turn.  Runs once per turn.
    /// </summary>
    public virtual void EnemyTurnStart()
    {

    }

    /// <summary>
    /// Toggles isPlayerTurn and triggers newTurn event for actors.
    /// </summary>
    public static void newTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        firstRun = true;
        numberOfTurns++;
    }

    /// <summary>
    /// Sets the turn, if player's turn set true.  Also trigger newturn event for actors.
    /// </summary>
    /// <param name="playerTurn">true = Player Turn / false = Enemy Turn</param>
    public static void newTurn(bool playerTurn)
    {
        isPlayerTurn = playerTurn;
        firstRun = true;
        numberOfTurns++;
    }

    /******************
     *  Set/Gets
     ******************/

    #region SetGet

    /// <summary>
    /// Returns the current number of turns passed
    /// </summary>
    /// <returns>current turn number as int</returns>
    public static int GetTurnNumber()
    {
        return numberOfTurns;
    }

    /// <summary>
    /// Returns true if it is the player's turn
    /// </summary>
    /// <returns>player turn as bool</returns>
    public static bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }


    #endregion
}
