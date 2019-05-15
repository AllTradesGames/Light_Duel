using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

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
        Debug.Log("Slashy triggered");
        if (other.tag == "dorito" )
        {
            UnityEngine.Plane plane = new UnityEngine.Plane(swordPoint.position, swordHilt.position, lastPoint);
            GameObject[] sliceyBoys = other.gameObject.SliceInstantiate(lastPoint, plane.normal);
            Debug.Log(sliceyBoys);
            for(int i = 0; i < sliceyBoys.Length; i++)
            {
                sliceyBoys[i].AddComponent<SlicesScript>();
            }

        }
    }
}
