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
    public int maxRolls;
    public string currentPlayerName;
    public string nextPlayerName;
    public int currentPlayerID;
    public float turnScore;

    //MatchType specific variables
    //CALLAO
    public float callao01 = 0;
    public float callao01Count = 0;
    public float callao02 = 0;
    public float callao02Count = 0;

    //BURDEL
    public float burdelStage = 0;

    //Tortuga
    public float tortugaStage = 0;

    //Managers

    public GameManager gameMGR; //Referencia al Game Manager
    public MatchManager matchMGR; //Referencia al Match Manager
    public RollManager rollMGR; //Referencia al Match Manager
    public UIManager uiMGR; //Referencia al UI Manager

    public MatchScore matchSCR; //Referencia al Match Score


    public Transform dadosSet;

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
        currentTurnNumber = 1;
        maxRolls = 0;
        uiMGR.UpdatePlayerTurns(currentTurnNumber, WhoCurrentTurnID(turnID), WhoNextTurnID(turnID));
        currentRolls = 0;// Cada turno inicia en cero


    }

    public void NextTurn()
    {
        currentTurnID++;
        currentTurnNumber++;
        currentRolls = 0;
        rollMGR.UpdateRollState(RollState.PreRoll);
        rollMGR.confirmedDados = 0;
        rollMGR.selectedDados = 0;
        rollMGR.rollScore = 0;
        rollMGR.addedScore = 0;
        rollMGR.highestValue = 0;
        callao01 = 0;
        callao02 = 0;
        //Release los dados para poder Load

        foreach (Transform dado in dadosSet)
        {
            dado.GetComponent<DadoScript>().isConfirmed = false;
            dado.GetComponent<DadoScript>().isSelected = false;
        }

        matchSCR.PopulateMs("update");

        if (currentTurnID > gameMGR.maxPlayers - 1) currentTurnID = 0;//si es mas que la cantidad de players, es el 0

        uiMGR.UpdatePlayerTurns(currentTurnNumber, WhoCurrentTurnID(currentTurnID), WhoNextTurnID(currentTurnID));
    }

    public void EndTurn()
    {
        rollMGR.UpdateRollState(RollState.PreRoll);
        turnScore = rollMGR.rollScore;
        gameMGR.playerList[currentTurnID].matchScore = turnScore;

        if (currentTurnNumber == gameMGR.maxPlayers)
        {
            matchMGR.EndMatch();
        }
        else
        {
            NextTurn();
        }

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