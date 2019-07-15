﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : Movement
{
    public GameObject AttackPreFab;
    public bool isLast;

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
        //TODO
        Debug.Log("Donut stabbed successfully");
        Instantiate(AttackPreFab, transform.position, transform.rotation);
        // TODO: Call network spawn attack
        //GameControl.SpawnStab();
    }

    void OnTargetFail()
    {
        Debug.Log("Donut slashed, oops");
    }


}


