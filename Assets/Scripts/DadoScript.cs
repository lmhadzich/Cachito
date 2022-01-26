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
    public Button LoadButton;
    public Button RollButton;
    public GameObject DadosSet;
    public TextMeshProUGUI dadoValueText;

    public RollManager rollMGR;
    public TurnManager turnMGR;

    public Material baseMaterial;
    public Material selectedMaterial;

    public bool isSelected;
    public bool isConfirmed;

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

    void LoadCachito() //Función al hacer click Botón de LoadCachito
    {
        
        if (isConfirmed != true) {    
            if (isSelected == false)
            {
                rollMGR.UpdateRollState(RollState.Loaded); //Updateamos el TurnSystem
                dadoValueText.text = "?"; //Cambia el texto
                Vector3 parentTransform = DadosSet.transform.position; // Captura posición de Dadoset y la guarda en una variable
                transform.rotation = Random.rotation; // Rota el dado randoml
                float startPos = parentTransform.x - (dadoID * 1.5f); // Define qué tan separados van a estar los dados el uno del otro solo en X
                transform.position = new Vector3(startPos, 5, 0); // Setea la posición inicial del dado en X(por dado) y Y(compartida)
                DadoRB.isKinematic = true;
            }
            else
            {
                isConfirmed = true;
                rollMGR.confirmedDados++;
            }
        }


        rollMGR.sleepingDados = 0; //Regresamos el sleep a cero
    }

    // Update is called once per frame
    void Update()
    {

        if (DadoRB.isKinematic==false) { //si se mueve
            if (DadoRB.IsSleeping()) //Si el dado esta quieto (¿El IsSleeping lo detecta Unity?)
            {

                CheckNumber(); //ejecutar función CheckNumber

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
        if (!isSelected)
        {
            GetComponent<MeshRenderer>().material = baseMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = selectedMaterial;
        }
    }

    void CheckNumber()
    {
        if (enMovimiento == true){
            //Lo declara sin movimiento
            enMovimiento = false;
            //Agrega este dado al count de dados estaticos
            rollMGR.sleepingDados++;

            int fullDados = rollMGR.sleepingDados + rollMGR.confirmedDados;
            //Revisa si ya todos los dados estan estaticos para pasar al RollState THINKING
            if (rollMGR.maxDados == fullDados)
            {
                rollMGR.UpdateRollState(RollState.Thinking);
            }

            Vector2 XZ = new Vector2(0, 0);
            float X = Mathf.Round(transform.localEulerAngles.x);
            float Y = Mathf.Round(transform.localEulerAngles.y);
            float Z = Mathf.Round(transform.localEulerAngles.z);

            XZ.Set(X, Z);

            int DadoScore = 0;

            // Verificar posición final de los dados y asignar puntaje
            if (XZ.x == 0 & XZ.y == 90)
            {
                DadoScore = 4;

            }
            else if (XZ.x == 270 & XZ.y == 0)
            {
                DadoScore = 6;
                {

                }
            }
            else if (XZ.x == 0 & XZ.y == 0)
            {
                DadoScore = 5;
                {

                }
            }
            else if (XZ.x == 90 & XZ.y == 0)
            {
                DadoScore = 1;
                {

                }
            }
            else if (XZ.x == 0 & XZ.y == 180)
            {
                DadoScore = 2;
                {

                }
            }
            else if (XZ.x == 0 & XZ.y == 270)
            {
                DadoScore = 3;
                {

                }
            }


            Debug.Log("Dado " + dadoID + " es " + DadoScore.ToString());
            dadoValueText.text = DadoScore.ToString();
            enMovimiento = false;
        }
        
    }
}
