using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : Movement
{

    void OnTriggerEnter(Collider other)
    {

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
                case 11:
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
        Debug.Log("Donut stabbed successfully");
        myPlayer.SpawnStab(transform.position, transform.rotation);
    }

    void OnTargetFail()
    {
        Debug.Log("Donut slashed, oops");
    }


}


