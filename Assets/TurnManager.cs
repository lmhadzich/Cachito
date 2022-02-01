using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public int currentTurnNumber;
    public int currentTurnID;
    public int numberOfPlayers;

    public int currentRolls; //Keep track de la cantidad de rolls
    public string currentPlayerName;
    public string nextPlayerName;
    public int currentPlayerID;
    public int currentPlayerSeat;
    public int leaderSeat;
    public float turnScore;

    public GameManager gameMGR; //Referencia al Game Manager
    public MatchManager matchMGR; //Referencia al Match Manager
    public RollManager rollMGR; //Referencia al Match Manager
    public UIManager uiMGR; //Referencia al UI Manager

    public int WhoCurrentTurnID(int id)
    {
        return id;
    }

    public int WhoNextTurnID(int id)
    {
        int nextID = id+1;
        if (nextID > gameMGR.maxPlayers-1) nextID = 0;//si es mas que la cantidad de players, es el 0
        return nextID;
    }


    public void StartTurn(int turnID)
    {
        currentTurnID = turnID;
        uiMGR.UpdatePlayerTurns(currentTurnNumber, WhoCurrentTurnID(turnID), WhoNextTurnID(turnID));
        currentRolls = 0;// Cada turno inicia en cero

    }

    public void NextTurn()
    {
        currentTurnID++;
        currentTurnNumber++;

        if (currentTurnID > gameMGR.maxPlayers - 1) currentTurnID = 0;//si es mas que la cantidad de players, es el 0

        uiMGR.UpdatePlayerTurns(currentTurnNumber, WhoCurrentTurnID(currentTurnID), WhoNextTurnID(currentTurnID));
    }

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }
}