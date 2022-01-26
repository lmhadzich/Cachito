using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public GameObject selector;
    public GameObject leader;
    public Vector3 center;
    public int Players;
    public float radius = 10;
    public GameObject target;
    
    void Start()
    {
       InstantiateCircle();
               
    }
    // Update is called once per frame
   
    void InstantiateCircle()
    {     
    float angle = 360f/(float)Players;
    for (int i = 0; i < Players; i++)
        { 
            Quaternion rotation = Quaternion.AngleAxis(i*angle, Vector3.up);
            Vector3 direction = rotation*Vector3.forward;
            Vector3 position = center + (direction*radius);
            Instantiate(prefab, position, rotation);
        }
    }

    void Update()
    { 
        target = GameObject.FindWithTag("Leader");
        selector.transform.LookAt(target.transform,Vector3.up);
        
              
    }
}
