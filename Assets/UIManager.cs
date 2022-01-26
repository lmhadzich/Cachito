using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    //Referencias a Managers
    public TurnManager turnMGR;
    
    //Referencias a UI Texts
    public TextMeshProUGUI txtGameState;
    public TextMeshProUGUI txtMatchState;
    public TextMeshProUGUI txtRollState;
    public TextMeshProUGUI txtMatchTurn;
    public TextMeshProUGUI txtCurrentRoll;
    public TextMeshProUGUI txtCurrentLeader;
    public TextMeshProUGUI txtCurrentPlayer;
    public TextMeshProUGUI txtNextPlayer;

    //Referencias a botones
    public GameObject btnStartGame;
    public GameObject btnSeatPlayers;
    public GameObject btnRollLeader;

    public GameObject btnLoad;
    public GameObject btnRoll;
    public GameObject btnEndSelection;

    void Awake()
    {
        Debug.Log("UIManagerStarted");
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged; //Empezar a escuchar al game manager
        MatchManager.OnMatchStateChanged += MatchManagerOnOnMatchStateChanged; //Empezar a escuchar al Match manager
        RollManager.OnRollStateChanged += RollManagerOnOnRollStateChanged;//Empezar a escuchar al Roll manager
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged; //Dejar de escuchar al game manager
        MatchManager.OnMatchStateChanged -= MatchManagerOnOnMatchStateChanged; //Dejar de escuchar al Match manager
        RollManager.OnRollStateChanged -= RollManagerOnOnRollStateChanged;//Empezar a escuchar al Roll manager
    }

    public void UpdatePlayerTurns(int turn,string current, string next)
    {
        txtCurrentPlayer.text = current;
        txtNextPlayer.text = next;
        txtMatchTurn.text = "TURN: "+turn.ToString();
    }

    private void GameManagerOnOnGameStateChanged(GameState state) //cuando cambia el state del GAME
    {
        txtGameState.text = "Game: " +state.ToString(); //Actualizamos el UI Text

        switch (state) //Acciones especificas segun el State del GAME
        {
            case GameState.StartMenu:
                break;
            case GameState.PlayerSeating:
                break;
            case GameState.LeaderRoll:
                //Ya se sentaron los players

                break;
            case GameState.Playing:
                break;
            case GameState.MatchEnd:
                break;
            case GameState.GameEnd:
                break;
        }

        //Solo mostramos ciertos botones dependiendo del GAME state
        if (state != GameState.StartMenu)
        {
            btnStartGame.SetActive(false);
        }
        else
        {
            btnStartGame.SetActive(true);
        }

        if (state != GameState.PlayerSeating)
        {
            btnSeatPlayers.SetActive(false);
        }
        else
        {
            btnSeatPlayers.SetActive(true);
        }

        if (state != GameState.LeaderRoll)
        {
            btnRollLeader.SetActive(false);
        }
        else
        {
            btnRollLeader.SetActive(true);
        }
    }

    private void MatchManagerOnOnMatchStateChanged(MatchState state) //Cuando cambia el  state del MATCH
    {
        txtMatchState.text = "Match: " + state.ToString() + " " + MatchManager.matchMGR.matchType; //Actualizamos el UI text

        switch (state)//Que pasa en cada MATCH state
        {
            case MatchState.NotStarted:
                //No se ha iniciado un Match todavia
                txtCurrentLeader.text = MatchManager.matchMGR.leaderName ; //deberia salir NO LEADER
                break;
            case MatchState.TypeSelection:
                //Estamos seleccionando el tipo de juego
                break;
            case MatchState.Playing:
                //Mostrar nombre + ID del leader
                txtCurrentLeader.text = MatchManager.matchMGR.leaderName + " (ID" + MatchManager.matchMGR.leaderID + ", SEAT" + MatchManager.matchMGR.leaderSeat + ")";
                break;
            case MatchState.Ended:
                //Ya se sentaron los players
                break;
        }
    }
    private void RollManagerOnOnRollStateChanged(RollState state) //Cuando cambia el  state del ROLL
    {
        txtRollState.text = "Roll: " + state.ToString();//Actualizamos el UI text
        txtCurrentRoll.text = "ROLL: " +turnMGR.currentRolls.ToString();//Actualizamos el UI text


        //Solo mostramos ciertos botones dependiendo del ROLL state
        if (state != RollState.PreRoll)
        {
            btnLoad.SetActive(false);
        }
        else
        {
            btnLoad.SetActive(true);
        }

        if (state != RollState.Loaded)
        {
            btnRoll.SetActive(false);
        }
        else
        {
            btnRoll.SetActive(true);
        }

        if (state != RollState.Thinking)
        {
            btnEndSelection.SetActive(false);
        }
        else
        {
            btnEndSelection.SetActive(true);
        }



        switch (state)//Que pasa en cada MATCH state
        {
            case RollState.PreRoll:
                break;
            case RollState.Loaded:
                break;
            case RollState.Rolling:
                break;
            case RollState.Thinking:
                break;
            case RollState.Selected:
                break;
        }
    }
}
