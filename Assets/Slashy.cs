using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slashy : MonoBehaviour
{
    public Transform swordPoint;
    public Transform swordHilt;
    private Vector3 lastPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastPoint = swordPoint.position;
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target" )
        {
            Debug.Log(other.tag);
            
        }
    }
}
