using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryMovement : AttackMovement
{
    private Transform target;
    private bool isParried = false;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isParried)
        {
            transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
            transform.LookAt(target.position);
        }
    }
    void FixedUpdate()
    {
        if (isParried)
        {
            lastPosition = new Vector3(transform.position.x, lastPosition.y, lastPosition.z - speed / 5f * Time.deltaTime);
            transform.position = lastPosition;
            transform.rotation = lastRotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "weapon":
                if (false/*!isParried*/)
                {
                    isParried = true;
                    transform.parent = other.transform;
                    lastRotation = transform.rotation;
                    lastPosition = transform.position;
                }
                break;
            case "death zone":
                Destroy(gameObject);
                break;

        }

    }
}