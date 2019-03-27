using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;
    private Vector3 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(player.position);
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * -speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "weapon" )
        {
            Debug.Log(other.tag);
            transform.Find("Particles").gameObject.SetActive(true);
            transform.Find("Donut").GetComponent<MeshRenderer>().enabled=false;
        }
    }


}


