using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorito : Movement
{
    public GameObject AttackPreFab;

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
        else if (other.tag == "death zone")
        {

            transform.Find("BadParticles").gameObject.SetActive(true);
            transform.Find("Donut").GetComponent<MeshRenderer>().enabled = false;
            OnTargetFail();
        }


    }

    void OnTargetSuccess()
    {
        Debug.Log("Dorito stabbed successfully");
        Instantiate(AttackPreFab, transform.position, transform.rotation);
        // TODO: Call network spawn attack
    }

    void OnTargetFail()
    {
        Debug.Log("Dorito slashed, oops");
    }


}


