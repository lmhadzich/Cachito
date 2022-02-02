using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//Trigger check para seleccionar los dados
public class SelectedArea : MonoBehaviour
{


    public MatchManager matchMGR;
    public TurnManager turnMGR;
    public UIManager uiMGR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckDadoValid(Collider dado)
    {
        MatchType currentMatchType = matchMGR.matchType;
        float dadoScore = dado.GetComponent<DadoScript>().DadoScore;

        switch (currentMatchType)
        {
            case MatchType.Callao:

                if (turnMGR.callao01 == 0)//si no se ha definido el valor 01
                {
                    turnMGR.callao01Count++; //aumentamos en 1 la cantidad de dados con el valor de callao01
                    turnMGR.callao01 = dadoScore;//ocupamos el valor de callao01 con este dado
                    dado.GetComponent<DadoScript>().isSelected = true;//permitimos seleccionar el dado
                    dado.GetComponent<DadoScript>().DadoValidVariable = "callao01";//identificamos que este dado es el que esta determinando el valor 01
                }
                else//si ya ha sido tomado el valor01, vemos si queda el 02
                {
                    if (turnMGR.callao02 == 0 & turnMGR.callao01 !=dadoScore)//si no se ha definido el valor 02 y el 01 no es el mismo score
                    {
                        turnMGR.callao02Count++;//aumentamos en 1 la cantidad de dados con el valor de callao02
                        turnMGR.callao02 = dadoScore;//ocupamos el valor de callao02 con este dado
                        dado.GetComponent<DadoScript>().isSelected = true;//permitimos seleccionar el dado
                        dado.GetComponent<DadoScript>().DadoValidVariable = "callao02";//identificamos que este dado es el que esta determinando el valor 01
                    }
                    else//si ya esta el 01 y el 02
                    {
                        if(dadoScore == turnMGR.callao01)// si su score coincide con callao01
                        {
                            dado.GetComponent<DadoScript>().isSelected = true;// estado selected
                            dado.GetComponent<DadoScript>().DadoValidVariable = "callao01";//identificamos que este dado es el que esta determinando el valor 01
                            turnMGR.callao01Count++;//aumentamos en 1 la cantidad de dados con el valor de callao01
                        }
                        else if (dadoScore == turnMGR.callao02)
                        {
                            dado.GetComponent<DadoScript>().isSelected = true;// estado selected
                            dado.GetComponent<DadoScript>().DadoValidVariable = "callao02";
                            turnMGR.callao02Count++;//aumentamos en 1 la cantidad de dados con el valor de callao02
                        }
                        else//si no coincide
                        {
                            dado.GetComponent<DadoScript>().isWrong = true;// estado wrong
                        }
                    }
                   
                }
                {
                }

                break;
            case MatchType.Burdel:
                break;
            case MatchType.Tortuga:
                break;
            case MatchType.OjosAzules:
                break;

        }
    }

    //On trigger, set isSelected a true a ese dado.
    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger)//entonces es el dado
        {
            CheckDadoValid(obj);
        }
            
    }

    public void ReleaseDado(Collider dado)
    {
        MatchType currentMatchType = matchMGR.matchType;
        float dadoScore = dado.GetComponent<DadoScript>().DadoScore;

        switch (currentMatchType)
        {
            case MatchType.Callao://Si es CALLAO
                dado.GetComponent<DadoScript>().isSelected = false;
                dado.GetComponent<DadoScript>().isWrong = false;

                if (dado.GetComponent<DadoScript>().DadoValidVariable == "callao01")//Si era uno de los dados de la variable callao01
                {
                    turnMGR.callao01Count--;//Quitamos uno de la lista
                    dado.GetComponent<DadoScript>().DadoValidVariable = "";//Le decimos que ya no ocupa callao01

                } else if (dado.GetComponent<DadoScript>().DadoValidVariable == "callao02")
                {
                    turnMGR.callao02Count--;//Quitamos uno de la lista
                    dado.GetComponent<DadoScript>().DadoValidVariable = "";//Le decimos que ya no ocupa callao02
                }

                if (turnMGR.callao01Count == 0)//SOLO si ya no hay ningun dado que corresponde a callao01
                {
                    turnMGR.callao01 = 0;//callao01 queda libre
                }
                if (turnMGR.callao02Count == 0)// SOLO si ya no hay ningun dado que corresponde a callao02
                {
                    turnMGR.callao02 = 0;//callao02 queda libre
                }


                break;
        }
    }
                    
        private void OnTriggerExit(Collider obj)
    {
        if (!obj.isTrigger)//entonces es el dado
        {
            ReleaseDado(obj);
        }
    }


    }

