using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DadoScript : MonoBehaviour
{

    public bool isStatic;
    public int dadoID;
    private Rigidbody DadoRB;
    private int rotationX;
    private int rotationY;
    private int rotationZ;
    public bool enMovimiento;
    public Button RollButton;
    public GameObject DadosSet;
    public TextMeshProUGUI dadoValueText;

    // Start is called before the first frame update
    void Start()
    {
        dadoValueText.text = "?"; 
        Debug.Log("DadoScript Started");
        DadoRB = gameObject.GetComponent<Rigidbody>();
       
        //Random Start rotation
        transform.rotation = Random.rotation;
        enMovimiento = false;

        //Boton de Roll
        Button RollBtn = RollButton.GetComponent<Button>();
        RollBtn.onClick.AddListener(reRollDado);
    }

    void reRollDado() //Función al hacer click Botón de Roll
    {

        dadoValueText.text = "?"; //Cambia el texto

        Vector3 parentTransform = DadosSet.transform.position; // Captura posición de Dadoset y la guarda en una variable
        
        Debug.Log("You have clicked the button!");
        transform.rotation = Random.rotation; // Rota el dado randomly

        float startPos = parentTransform.x - (dadoID*1.5f); // Define qué tan separados van a estar los dados el uno del otro solo en X
        transform.position = new Vector3(startPos, 5, 0); // Setea la posición inicial del dado en X(por dado) y Y(compartida)
    }

    // Update is called once per frame
    void Update()
    {
        if (DadoRB.IsSleeping()) //Si el dado esta quieto (¿El IsSleeping lo detecta Unity?)
        {
            
            CheckNumber(); //ejecutar función CheckNumber
            gameObject.tag = "drag"; // Los hace arrastrables
        }
        else{
            //Debug.Log("En Movimiento");
            enMovimiento = true;
            gameObject.tag = "Untagged"; // Los hace no arrastrables
        }
    }

    void CheckNumber()
    {
        if (enMovimiento == true){

            //Debug.Log(dadoID + " stopped");
            Vector2 XZ = new Vector2(0, 0);
            float X = Mathf.Round(transform.localEulerAngles.x);
            float Y = Mathf.Round(transform.localEulerAngles.y);
            float Z = Mathf.Round(transform.localEulerAngles.z);

            //Debug.Log("X> " + X + " Y> " + Y + " Z> " + Z);

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
