using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public int currentTurn;
    public int numberOfPlayers;

    public int currentRolls; //Keep track de la cantidad de rolls
    public string currentPlayerName;
    public string nextPlayerName;
    public int currentPlayerID;
    public int currentPlayerSeat;
    public int leaderSeat;

    public GameManager gameMGR; //Referencia al Game Manager
    public MatchManager matchMGR; //Referencia al Match Manager
    public RollManager rollMGR; //Referencia al Match Manager
    public UIManager uiMGR; //Referencia al UI Manager

    public void GetCurrentPlayer(int turno)
    {
        int turnoIndex = turno - 1;
        currentPlayerName = gameMGR.playerList[turnoIndex].name;
        nextPlayerName= gameMGR.playerList[turnoIndex + 1].name;

        uiMGR.UpdatePlayerTurns(turno,currentPlayerName,nextPlayerName);

    }

    public void StartTurn(int lastTurn)
    {
        currentTurn = lastTurn + 1;
        GetCurrentPlayer(currentTurn);
        rollMGR.NewRoll();
    }

    private void Awake()
    {
        numberOfPlayers = gameMGR.playerNumber;

    }

    private void Start()
    {
        Debug.Log(numberOfPlayers);
        currentRolls = 0;// Cada turno inicia en cero
    }

}