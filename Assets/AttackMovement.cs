using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool isLast;
    public string targetTag;
    protected Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((target.position-transform.position).normalized * speed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
