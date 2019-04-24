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

        Debug.Log(other.gameObject.name);

        if (other.tag == "weapon")
        {
            Collider[] colliders;
            colliders = transform.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }

            switch (other.gameObject.layer)
            {
                case 9:
                    transform.Find("GoodParticles").gameObject.SetActive(true);
                    transform.Find("Donut").GetComponent<MeshRenderer>().enabled = false;
                    OnTargetSuccess();
                    break;
                case 10:
                    transform.Find("BadParticles").gameObject.SetActive(true);
                    transform.Find("Donut").GetComponent<MeshRenderer>().enabled = false;
                    OnTargetFail();
                    break;
            }
        }
    }

    void OnTargetSuccess()
    {
        Debug.Log("Donut stabbed successfully");
    }

    void OnTargetFail()
    {
        Debug.Log("Donut slashed, oops");
    }


}


