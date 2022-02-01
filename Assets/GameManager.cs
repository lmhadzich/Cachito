using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    //Iniciamos el Game Manager
    public static GameManager gameMGR; //Permite agarrarlo de cualquier parte del game.
    public MatchManager matchMGR; //Permite agarrarlo de cualquier parte del game.
    public GameState State; //Permite modificar el gameMGR.State
    public static event Action<GameState> OnGameStateChanged; //Crea la function para avisar a otros script que se cambio el gameMGR.State

    public UIManager uiMGR; //Permite agarrarlo de cualquier parte del game.

    //List of players
    public List<Player> playerList; //Lista para insertar a los jugadores
    public int maxPlayers; //Cuantos van a jugar, lo determinamos por el count de playerList
    public int startingLeader; //Para testeo, cambiar en INSPECTOR

    public int maxGamePoints; //A cuantos puntos vamos a jugar

    private void Awake()
    {
        gameMGR = this; //Asignar este GameManager como referencia.
        maxGamePoints = 3; //Vamos a jugar a 3 puntos
    }
    private void Start()
    {
        UpdateGameState(GameState.StartMenu); //Avisamos que estamos en el StartMenu

        //Agregamos players con su data
        playerList = new List<Player>();


    }
    public void UpdateGameState(GameState newState) //Funcion global para actualizar el Game State
    {
        State = newState; //Recibe el nuevo state

        switch (newState) // Dependiendo del state, hacer algo
        {
            case GameState.StartMenu:
                //Agregamos jugadores y cosas asi
                break;
            case GameState.PlayerSeating:
                //Los players seleccionan su silla
                break;
            case GameState.LeaderRoll:
                //Se hace el roll para ver quien inicia como leader
                break;
            case GameState.Playing:
                //Se juegan los matches
                break;
            case GameState.GameStatus:
                //Updatear gamePoints, mostrar Scoreboard
                break;
            case GameState.GameEnd:
                //Mostrar scoreboard final, determinar ganador
                break;
        }
        OnGameStateChanged?.Invoke(newState); //Si hay alguien listening a esto, ejecutar la funcion.
        Debug.Log("gameMGR State changed to: " + newState.ToString());
    }

    public void SelectPlayers()
    {
        GameManager.gameMGR.UpdateGameState(GameState.PlayerSeating);
        Debug.Log("Add Players Interface");

        playerList.Add(new Player("JapiChop"));
        playerList.Add(new Player("Pepino"));
        playerList.Add(new Player("Ruks"));

        

        //Actualizamos el playerNumber
        maxPlayers = playerList.Count;

        int playerIndex = 0;
        //Debugeamos quien esta en que silla
        foreach (Player plyr in playerList)
        {
            print(plyr.name + " es " + playerIndex);
            playerIndex++;
        }
    }

    public void RollForLeader()
    {
        int newLeader;
        GameManager.gameMGR.UpdateGameState(GameState.LeaderRoll);
        Debug.Log("Roll For Leader Interface");
        newLeader = Random.Range(0, maxPlayers); //por ahora Random
        matchMGR.currentLeaderID = newLeader;
        Debug.Log("El random leader es " + playerList[newLeader].name + " ID" + newLeader);
        uiMGR.UpdateLeaderUI();
    }

}
public enum GameState
{
    StartMenu,
    PlayerSeating,
    LeaderRoll,
    Playing,
    GameStatus,
    GameEnd
}