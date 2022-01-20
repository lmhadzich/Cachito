using UnityEngine;
using TMPro;

//Crear los estados del turno
public enum TurnState { PRE, LOADED, ROLLING, THINKING, SELECTED, ENDED }
public class TurnSystem : MonoBehaviour
{

    public TurnState globalTurnState;
    public TextMeshProUGUI TurnStateUI;
    public GameObject DadosSet;
    public int maxDados;
    public int selectedDados;
    public int sleepingDados;
    void ContarDados()
    {
        maxDados = DadosSet.transform.childCount;
        Debug.Log("Hay " + maxDados + " dados");
    }

    public void isThinking()
    {
        if (sleepingDados == maxDados)
        {
            UpdateTurnState(TurnState.THINKING);
        }
    }

    public void UpdateTurnState(TurnState newTurnState)
    {
        globalTurnState = newTurnState;
        TurnStateUI.text = globalTurnState.ToString();
        Debug.Log(globalTurnState);

    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateTurnState(TurnState.ROLLING);
        ContarDados();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
