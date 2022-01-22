using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Iniciamos el Game Manager
    public static GameManager gameMGR; //Permite agarrarlo de cualquier parte del game.
    public GameState State; //Permite modificar el gameMGR.State
    public static event Action<GameState> OnGameStateChanged; //Crea la function para avisar a otros script que se cambio el gameMGR.State


    //List of players
    public List<Player> playerList; //Lista para insertar a los jugadores
    public int playerNumber; //Cuantos van a jugar, lo determinamos por el count de playerList
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
        playerList.Add(new Player(1,"Pietro", 1));
        playerList.Add(new Player(2, "Luismi", 2));
        playerList.Add(new Player(3, "Ruks", 3));

        //Actualizamos el playerNumber
        playerNumber = playerList.Count;

        //Debugeamos quien esta en que silla
        foreach (Player plyr in playerList)
        {
            print(plyr.name + " esta en el seat " + plyr.seat);
        }
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
            case GameState.MatchEnd:
                //Updatear gamePoints, mostrar Scoreboard
                break;
            case GameState.GameEnd:
                //Mostrar scoreboard final, determinar ganador
                break;
        }
        OnGameStateChanged?.Invoke(newState); //Si hay alguien listening a esto, ejecutar la funcion.
        Debug.Log("gameMGR State changed to: " + newState.ToString());
    }

    public void StartGame()
    {
        GameManager.gameMGR.UpdateGameState(GameState.PlayerSeating);
    }
 
    public void SeatPlayers()
    {
        GameManager.gameMGR.UpdateGameState(GameState.LeaderRoll);
    }

    public void RollLeader()
    {
        GameManager.gameMGR.UpdateGameState(GameState.Playing);
        MatchManager.matchMGR.StartMatch(startingLeader);
    }
}
public enum GameState
{
    StartMenu,
    PlayerSeating,
    LeaderRoll,
    Playing,
    MatchEnd,
    GameEnd
}