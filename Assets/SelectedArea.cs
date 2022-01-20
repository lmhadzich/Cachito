using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger check para seleccionar los dados
public class SelectedArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //On trigger, set isSelected a true a ese dado.
    private void OnTriggerEnter(Collider Dado)
    {
        Dado.GetComponent<DadoScript>().isSelected = true;
    }

    //On exit, set isSelected a FALSE a ese dado.
    private void OnTriggerExit(Collider Dado)
    {
        Dado.GetComponent<DadoScript>().isSelected = false;
    }


}
