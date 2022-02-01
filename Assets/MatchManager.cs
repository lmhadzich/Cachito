using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{

    //Manage the players
    public int currentLeaderID; //El index del jugador leader
    public int nextLeaderID; //El index del siguiente leader


    public MatchType matchType; //Activa al lista de juegos que esta abajo
    public int maxRolls; //Guarda el maxRolls determinado por el leader

    public GameManager gameMGR; //Para referenciar en el inspector
    public static MatchManager matchMGR; //Permite agarrarlo de cualquier parte del game.
    public TurnManager turnMGR; //Permite agarrarlo de cualquier parte del game.
    public RollManager rollMGR;
    public MatchState State; //Permite modificar el matchMGR.State
    public static event Action<MatchState> OnMatchStateChanged; //Crea la function para avisar a otros script que se cambio el matchMGR.State

    public float Number1 = 0.01f;
    public float Number2 = 0.02f;
    public float Number3 = 0.03f;
    public float Number4 = 0.04f;
    public float Number5 = 0.05f;
    public float Number6 = 0.06f;
  
 
public void UpdateMatchType(MatchType newType)
    {
        matchType = newType;
     switch (newType) // Dependiendo del state, hacer algo
        {
            case MatchType.Callao:
                Number1 = 0.07f;
                break;
            case MatchType.Tortuga:
                Number1 = 0.01f;
                break;
            case MatchType.Burdel:
                Number1 = 0.01f;
                break;
            case MatchType.OjosAzules:
                Number2 = 0f;
                Number5 = 0f;
                break;
            
        }
 }

    private void Awake()
    {
        matchMGR = this;//Crear la instancia
        nextLeaderID = 0;//Al inicio no hay leader
    }

    public void UpdateMatchState(MatchState newState)
    {
        State = newState; //Recibe el nuevo state

        switch (newState) // Dependiendo del state, hacer algo
        {
            case MatchState.NotStarted:
                break;
            case MatchState.TypeSelection:
                break;
            case MatchState.Playing:
                break;
            case MatchState.Ended:
                break;

        }
        OnMatchStateChanged?.Invoke(newState); //Si hay alguien listening a esto, ejecutar la funcion.
        Debug.Log("Match State changed to: " + newState.ToString());
    } //Funcion global para actualizar el MATCH state

    private void Start()
    {
        matchMGR.UpdateMatchState(MatchState.NotStarted);//Arrancar con el match state NOT STARTED

    }
    public void StartMatch() //Iniciar un match
    {
        UpdateMatchType(MatchType.Callao);
        gameMGR.UpdateGameState(GameState.Playing); //Avisamos que ya estamos playing un Match
        UpdateMatchState(MatchState.Playing); //Avisamos que ya estamos playing un Match
        Debug.Log("Match Started!");

        turnMGR.StartTurn(currentLeaderID);
    }
}

public enum MatchState
{
    NotStarted,
    TypeSelection,
    Playing,
    Ended
}
public enum MatchType { 
    Undefined,
    Callao, 
    Burdel, 
    Tortuga,
    OjosAzules
    }