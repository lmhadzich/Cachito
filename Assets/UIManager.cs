using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    //Referencias a Managers
    public GameManager gameMGR;
    public MatchManager matchMGR;
    public TurnManager turnMGR;
    public RollManager rollMGR;
    
    //Referencias a UI Texts
    public TextMeshProUGUI txtGameState;
    public TextMeshProUGUI txtMatchState;
    public TextMeshProUGUI txtRollState;
    public TextMeshProUGUI txtMatchTurn;
    public TextMeshProUGUI txtCurrentRoll;
    public TextMeshProUGUI txtCurrentLeader;
    public TextMeshProUGUI txtCurrentPlayer;
    public TextMeshProUGUI txtNextPlayer;
    public TextMeshProUGUI txtRollScore;
    public TextMeshProUGUI txtTurnScore;

    //Referencias a botones
    public GameObject btnSelectPlayers;
    public GameObject btnRollForLeader;
    public GameObject btnStartMatch;

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
    void Update()
    {
       txtTurnScore.text = turnMGR.turnScore.ToString();
       txtRollScore.text = rollMGR.rollScore.ToString();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged; //Dejar de escuchar al game manager
        MatchManager.OnMatchStateChanged -= MatchManagerOnOnMatchStateChanged; //Dejar de escuchar al Match manager
        RollManager.OnRollStateChanged -= RollManagerOnOnRollStateChanged;//Empezar a escuchar al Roll manager
    }

    public void UpdatePlayerTurns(int turnNumber,int current, int next)
    {
        txtCurrentPlayer.text = gameMGR.playerList[current].name +" ( " + current + ")";
        txtNextPlayer.text = gameMGR.playerList[next].name + " ( " + next + ")";
        txtMatchTurn.text = "TURN: "+turnNumber.ToString();
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
            case GameState.GameStatus:
                break;
            case GameState.GameEnd:
                break;
        }

        //Solo mostramos ciertos botones dependiendo del GAME state
        if (state != GameState.StartMenu)
        {
            btnSelectPlayers.SetActive(false);
        }
        else
        {
            btnSelectPlayers.SetActive(true);
        }

        if (state != GameState.PlayerSeating)
        {
            //btnRollForLeader.SetActive(false);
        }
        else
        {
            //btnRollForLeader.SetActive(true);
        }

        if (state != GameState.LeaderRoll)
        {
            //btnStartMatch.SetActive(false);
        }
        else
        {
            //btnStartMatch.SetActive(true);
        }
    }

    public void UpdateLeaderUI()
    {
        int currentLeader = matchMGR.currentLeaderID;
        txtCurrentLeader.text = gameMGR.playerList[currentLeader].name + " (ID " + currentLeader + ")";
    }

    private void MatchManagerOnOnMatchStateChanged(MatchState state) //Cuando cambia el  state del MATCH
    {
        txtMatchState.text = "Match: " + state.ToString() + " " + MatchManager.matchMGR.matchType; //Actualizamos el UI text

        switch (state)//Que pasa en cada MATCH state
        {
            case MatchState.NotStarted:
                //No se ha iniciado un Match todavia
                txtCurrentLeader.text = "NO LEADER";
                break;
            case MatchState.TypeSelection:
                //Estamos seleccionando el tipo de juego
                break;
            case MatchState.Playing:
                //Mostrar nombre + ID del leader
                
                txtMatchState.text = "Match: " + state.ToString() + " " + MatchManager.matchMGR.matchType;
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
        if (state != RollState.Thinking)
        {
        }
        else
        {
        }

        if (state != RollState.Loaded)
        {

        }
        else
        {

        }

        if (state != RollState.Thinking)
        {
        }
        else
        {
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
