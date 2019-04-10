using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicesScript : MonoBehaviour
{
    public float force = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>().AddForce(transform.forward * force);
        gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
