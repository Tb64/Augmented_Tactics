using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBehavoir : MonoBehaviour {

    static bool isPlayerTurn;
    static int numberOfTurns;

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

    public virtual void LateUpdate()
    {
        if(firstRun)
        {
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

    }

    /// <summary>
    /// Runs on the start of Enemy's Turn.  Runs once per turn.
    /// </summary>
    public virtual void EnemyTurnStart()
    {

    }

    public static void newTurn()
    {
        firstRun = true;
        numberOfTurns++;
    }

    /// <summary>
    /// Sets the turn, if player's turn set true
    /// </summary>
    /// <param name="playerTurn">true = Player Turn / false = Enemy Turn</param>
    public static void newTurn(bool playerTurn)
    {
        isPlayerTurn = playerTurn;
        newTurn();
    }
}
