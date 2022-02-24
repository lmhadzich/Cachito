using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{



    public GameManager gameMGR; //Para referenciar en el inspector
    public MatchManager matchMGR; //Permite agarrarlo de cualquier parte del game.
    public MatchScore matchSCR; //Permite agarrarlo de cualquier parte del game.
    public TurnManager turnMGR; //Permite agarrarlo de cualquier parte del game.
    public static RollManager rollMGR; //Permite agarrarlo de cualquier parte del game.

    public RollState State; //Permite modificar el matchMGR.State
    public static event Action<RollState> OnRollStateChanged; //Crea la function para avisar a otros script que se cambio el rollMGR.State

    public GameObject setDados;
    public int maxDados;
    public int selectedDados;
    public int confirmedDados;
    public int sleepingDados;
    public float addedScore;
    public float rollScore;
    public float highestValue;

    private void Start()
    {
        
        maxDados  = setDados.transform.childCount;
        sleepingDados = 0;
        UpdateRollState(RollState.PreRoll);
        
    }

    public void CheckIfLastRoll()
        {
        if (matchMGR.currentLeaderID == turnMGR.currentTurnID)
        {
            Debug.Log("LEader ROLL");
            if (turnMGR.currentRolls == 3)//Ultimo Roll
            {
                Debug.Log("LEader LAST");
                UpdateRollState(RollState.LastRoll);
            }
        }

        if (matchMGR.currentLeaderID != turnMGR.currentTurnID)
        {
            if (turnMGR.currentRolls == turnMGR.maxRolls)//Ultimo Roll
            {
                Debug.Log("Not Leader Max Roll LAST");
                UpdateRollState(RollState.LastRoll);
            }
        }



    }
    public void NextRoll()
    {
        turnMGR.currentRolls++;

        if (turnMGR.currentRolls == 4)
        {

            turnMGR.NextTurn();
            //Debug.Log("siguiente turno");
        }

        //CheckIfLastRoll();


        if (turnMGR.currentTurnID == matchMGR.currentLeaderID)//Si el que juega es el leader, modifica el maxRolls
        {
            turnMGR.maxRolls = turnMGR.currentRolls;
        }
        
        else
        {
            if(turnMGR.currentRolls > turnMGR.maxRolls)
                turnMGR.NextTurn();
            //Debug.Log("siguiente turno");
        }
                    
        rollMGR.UpdateRollState(RollState.Loaded);
        
    }

    public void NewRoll()
    {

        
    }


    public void UpdateRollState(RollState newState)
    {
        State = newState; //Recibe el nuevo state

        switch (newState) // Dependiendo del state, hacer algo
        {
            case RollState.PreRoll:
                Debug.Log("Roll State changed to: " + newState.ToString());
                break;
            case RollState.Loaded:
                Debug.Log("Roll State changed to: " + newState.ToString());
                break;
            case RollState.Rolling:
                Debug.Log("Roll State changed to: " + newState.ToString());
                break;
            case RollState.Thinking:
                Debug.Log("Roll State changed to: " + newState.ToString());
                break;
            case RollState.Selected:
                Debug.Log("Roll State changed to: " + newState.ToString());
                //Dados marcar como selected los del trigger
                break;
            case RollState.LastRoll:
                Debug.Log("Roll State changed to: " + newState.ToString());
                break;

        }
        OnRollStateChanged?.Invoke(newState); //Si hay alguien listening a esto, ejecutar la funcion.
        
    } //Funcion global para actualizar el MATCH state

    private void Awake()
    {
        rollMGR = this; //Asignar este GameManager como referencia.
    }

    public void UpdateScore(float dadoScore)
    {
        if (turnMGR.callao01>=turnMGR.callao02)
            {
            highestValue = turnMGR.callao01;
            }
        else 
            {
                highestValue = turnMGR.callao02;
            }
          
        MatchType currentMatchType = matchMGR.matchType;
        addedScore = addedScore + dadoScore; //suma los valores de los dados
        switch (currentMatchType)
        {
            case MatchType.Callao:
                rollScore = selectedDados + highestValue;
                break;
            case MatchType.OjosAzules:
                rollScore = addedScore * -1;
                break;
            case MatchType.Undefined:
                rollScore = selectedDados + addedScore;
                break;
        }
        gameMGR.playerList[turnMGR.currentTurnID].matchScore = rollScore;
        //Debug.Log(gameMGR.playerList[turnMGR.currentTurnID].matchScore);
        matchSCR.PopulateMs("update");
        
    }

    private void Update()
    {
        if (gameMGR.playerList.Count > 0)
        {
            UpdateScore(0);
        }

    }
}
public enum RollState
{
    PreRoll,
    Loaded,
    Rolling,
    Thinking,
    Selected,
    LastRoll
}