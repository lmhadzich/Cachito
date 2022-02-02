using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{



    public GameManager gameMGR; //Para referenciar en el inspector
    public MatchManager matchMGR; //Permite agarrarlo de cualquier parte del game.
    public TurnManager turnMGR; //Permite agarrarlo de cualquier parte del game.
    public static RollManager rollMGR; //Permite agarrarlo de cualquier parte del game.
    public RollState State; //Permite modificar el matchMGR.State
    public static event Action<RollState> OnRollStateChanged; //Crea la function para avisar a otros script que se cambio el rollMGR.State

    public GameObject setDados;
    public int maxDados;
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

    public void NextRoll()
    {
        turnMGR.currentRolls++;
        if (turnMGR.currentTurnID == matchMGR.currentLeaderID)//Si el que juega es el leader, modifica el maxRolls
        {
            turnMGR.maxRolls = turnMGR.currentRolls;
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
                break;
            case RollState.Loaded:
                break;
            case RollState.Rolling:
                break;
            case RollState.Thinking:
                //Dados add drag
                break;
            case RollState.Selected:
                //Dados marcar como selected los del trigger
                break;

        }
        OnRollStateChanged?.Invoke(newState); //Si hay alguien listening a esto, ejecutar la funcion.
        Debug.Log("Roll State changed to: " + newState.ToString());
    } //Funcion global para actualizar el MATCH state

    private void Awake()
    {
        rollMGR = this; //Asignar este GameManager como referencia.
    }

    private void Update()
    {
        MatchType currentMatchType = matchMGR.matchType;
        switch (currentMatchType)
        {
            case MatchType.Callao:
                rollScore = confirmedDados + highestValue;
                break;
            case MatchType.OjosAzules:
                rollScore = addedScore * -1;
                break;
            case MatchType.Undefined:
                rollScore = confirmedDados + addedScore;
                break;
        }
    }
}
public enum RollState
{
    PreRoll,
    Loaded,
    Rolling,
    Thinking,
    Selected
}