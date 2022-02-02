using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DadoScript : MonoBehaviour
{

    public int dadoID;
    private Rigidbody DadoRB;
    private int rotationX;
    private int rotationY;
    private int rotationZ;
    public bool enMovimiento;
    public string dadoNum = "TBD";

    public int cachitoLoadHeight = 10;
    public Button LoadButton;
    public Button RollButton;
    public GameObject DadosSet;
    public float DadoScore = 0f;
    public TextMeshProUGUI dadoValueText;

    public RollManager rollMGR;
    public TurnManager turnMGR;
    public MatchManager matchMGR;
    public MatchScore matchSCR;
    public GameManager gameMGR;

    public Material baseMaterial;
    public Material selectedMaterial;
    public Material wrongMaterial;

    public bool isSelected;
    public bool isConfirmed;
    public bool isWrong;

    public string DadoValidVariable;

    // Start is called before the first frame update
    void Start()
    {

        //Resetear seleccion
        isConfirmed = false;
        isSelected = false;
        GetComponent<MeshRenderer>().material = baseMaterial; //Aplicar material Unselected

        dadoValueText.text = "?"; 
        DadoRB = gameObject.GetComponent<Rigidbody>();
       
        //Random Start rotation
        transform.rotation = Random.rotation;
        enMovimiento = false;

        DadoRB.isKinematic = true;


        //Boton de Load
        Button LoadBtn = LoadButton.GetComponent<Button>();
        LoadBtn.onClick.AddListener(LoadCachito);

        //Boton de Roll
        Button RollBtn = RollButton.GetComponent<Button>();
        RollBtn.onClick.AddListener(RollCachito);
    }

    void RollCachito()
    {
        DadoRB.isKinematic = false;
        rollMGR.UpdateRollState(RollState.Rolling);
    }

    void FixDuda() //lo que queramos que pase cuando un dado no cae en ningun trigger
    {
        float actualX = DadoRB.position.x;
        float actualY= DadoRB.position.y;
        float actualZ= DadoRB.position.z;

        //por ahora lo sube 5f y vuelve a caer
        Vector3 newPos = new Vector3(actualX, 5f, actualZ);
        enMovimiento = true;
        DadoRB.MovePosition(newPos);
        
        Debug.Log("FIXING " + dadoID);
        UpdateDadoScore();
    }

    public void UpdateDadoScore() 
        //Funcion para updatear el score. El trigger de cada dado lo ejecuta OnTriggerEnter
        //asi que se actualiza en tiempo real y solo cada vez que cambia de numero.
    {
        if (dadoNum == "CUATRO")
        {
            DadoScore = matchMGR.Number4;

        }
        else if (dadoNum == "SEIS")
        {
            DadoScore = matchMGR.Number6;
            {

            }
        }
        else if (dadoNum == "CINCO")
        {
            DadoScore = matchMGR.Number5;
            {

            }
        }
        else if (dadoNum == "UNO")
        {
            DadoScore = matchMGR.Number1;

        }
        else if (dadoNum == "DOS")
        {
            DadoScore = matchMGR.Number2;
            {

            }
        }
        else if (dadoNum == "TRES")
        {
            DadoScore = matchMGR.Number3;
            {

            }
        }

        dadoValueText.text = DadoScore.ToString();

    }

    void LoadCachito() //Función al hacer click Botón de LoadCachito
    {
        
        if (isConfirmed != true) {    
            if (isSelected == false)
            {
                rollMGR.UpdateRollState(RollState.Loaded); //Updateamos el TurnSystem
                dadoValueText.text = "?"; //Cambia el texto
                Vector3 parentTransform = DadosSet.transform.position; // Captura posición de Dadoset y la guarda en una variable
                transform.rotation = Random.rotation; // Rota el dado randoml
                float startPos = -5 + (dadoID * 1.5f); // Define qué tan separados van a estar los dados el uno del otro solo en X
                transform.position = new Vector3(startPos, cachitoLoadHeight, 0); // Setea la posición inicial del dado en X(por dado) y Y(compartida)
                DadoRB.isKinematic = true;
            }
            else
            {
                ConfirmarDado();

            }
        }

        rollMGR.sleepingDados = 0; //Regresamos el sleep a cero
    }

    public void ConfirmarDado()
    {
        if (isConfirmed != true)
        {
            if (isSelected == true)
            {
                isConfirmed = true;
                rollMGR.confirmedDados++;

            }
        }

        rollMGR.UpdateScore(DadoScore);
    }

    // Update is called once per frame
    void Update()
    {

        if (DadoRB.isKinematic==false) { //si se mueve
            if (DadoRB.IsSleeping()) //Si el dado esta quieto (¿El IsSleeping lo detecta Unity?)
            {

                UpdateSleepers(); //ejecutar función CheckNumber

                if (rollMGR.State == RollState.Thinking && isConfirmed != true) //Si estan quietos & en THINKING state
                {
                    gameObject.tag = "drag"; // Los hace arrastrables
                }
                else
                {
                    gameObject.tag = "Untagged"; // Los hace no arrastrables
                }

            }
            else
            {
                if (rollMGR.State == RollState.Thinking)
                {
                    enMovimiento = false;
                }
                else
                {
                    enMovimiento = true;
                }

            }
        }


        //Cambia de materiales dependiendo si esta Selected o no
        if (isSelected)
        {
            GetComponent<MeshRenderer>().material = selectedMaterial;
        }
        else if (isWrong)
        {
            GetComponent<MeshRenderer>().material = wrongMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = baseMaterial;
        }
        
        if (isSelected == true)
            {
             if (DadoScore>=rollMGR.highestValue)
                {
                    rollMGR.highestValue = DadoScore;
                }
            }    
}

    void UpdateSleepers()
    {
        if (enMovimiento == true) {

            //Lo declara sin movimiento
            enMovimiento = false;
            
            if (dadoNum == "TBD") // si no cae en un trigger
            {
                FixDuda();
            }

            UpdateDadoScore();
            enMovimiento = false;

            //Agrega este dado al count de dados estaticos
            rollMGR.sleepingDados++;

            int fullDados = rollMGR.sleepingDados + rollMGR.confirmedDados;
            //Revisa si ya todos los dados estan estaticos para pasar al RollState THINKING
            if (rollMGR.maxDados == fullDados)
            {
                rollMGR.UpdateRollState(RollState.Thinking);
            }

        }

    }
}

