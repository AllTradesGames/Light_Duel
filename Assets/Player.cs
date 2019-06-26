using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "comet":
                Destroy(other.gameObject);
                TakeDamage(1);
                break;
            case "slash attack":
                Destroy(other.gameObject);
                TakeDamage(1);
                break;
        }

    }

    void TakeDamage(int amount)
    {
        Debug.Log("ouch i took damage");
    }
}

