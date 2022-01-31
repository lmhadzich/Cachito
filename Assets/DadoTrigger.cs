using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadoTrigger : MonoBehaviour
{

    GameObject parentGO;
    public string sideNumber = "UNO";

    // Start is called before the first frame update
    void Start()
    {
    }
void OnTriggerEnter(Collider piso)
    {

        if (piso.CompareTag("Floor"))
        {
            this.transform.parent.GetComponent<DadoScript>().dadoNum = sideNumber;
            this.transform.parent.GetComponent<DadoScript>().UpdateDadoScore();
        }

    }

    void OnTriggerExit(Collider piso)
    {

        if (piso.CompareTag("Floor"))
        {
            this.transform.parent.GetComponent<DadoScript>().dadoNum = "TBD";
        }

    }
}
