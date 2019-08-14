using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;
    private Vector3 direction;
    public float speed;

    protected MoveHead myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
       // transform.LookAt(player.position);
        direction = transform.forward;
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<MoveHead>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * -speed * Time.deltaTime);
    }

}


