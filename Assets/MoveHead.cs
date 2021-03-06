﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class MoveHead : MovementHeadBehavior
{
    private Transform head;
    private Transform leftHand;
    private Transform rightHand;

    private GameControl controlScript;
    private ComboController comboScript;

    public int team;
    public GameObject DonutAttackPreFab;
    public GameObject DoritoAttackPreFab;


    // Start is called before the first frame update
    void Start()
    {
        controlScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        comboScript = GameObject.FindGameObjectWithTag("ComboController").GetComponent<ComboController>();
        if (networkObject.IsOwner)
        {
            Destroy(transform.Find("RemotePlayer").gameObject);
            head = transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            leftHand = transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor");
            rightHand = transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");
        }
        else
        {
            Destroy(transform.Find("OVRCameraRig").gameObject);
            transform.Find("RemotePlayer").gameObject.SetActive(true);
            head = transform.Find("RemotePlayer/CenterEyeAnchor");
            leftHand = transform.Find("RemotePlayer/LeftHandAnchor");
            rightHand = transform.Find("RemotePlayer/RightHandAnchor");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject.IsOwner)
        {
            networkObject.headPosition = head.position;
            networkObject.headRotation = head.rotation;
            networkObject.leftPosition = leftHand.position;
            networkObject.leftRotation = leftHand.rotation;
            networkObject.rightPosition = rightHand.position;
            networkObject.rightRotation = rightHand.rotation;
        }
        else
        {
            head.position = networkObject.headPosition;
            head.rotation = networkObject.headRotation;
            leftHand.position = networkObject.leftPosition;
            leftHand.rotation = networkObject.leftRotation;
            rightHand.position = networkObject.rightPosition;
            rightHand.rotation = networkObject.rightRotation;
        }

    }

    public void SendReadyRPC()
    {
        networkObject.SendRpc(RPC_READY, Receivers.Others);
    }

    public override void Ready(RpcArgs args)
    {
        controlScript.OnOpponentFound(true);
    }

    public void SpawnStab(Vector3 position, Quaternion rotation)
    {
        networkObject.SendRpc(RPC_SPAWN_STAB, Receivers.All, position, rotation, team);
    }

    public override void SpawnStab(RpcArgs args)
    {
        Vector3 position = args.GetNext<Vector3>();
        Quaternion rotation = args.GetNext<Quaternion>();
        int attackTeam = args.GetNext<int>();
        string targetTag = team == attackTeam ? "Enemy" : "Player";
        Instantiate(DonutAttackPreFab, position, rotation).GetComponent<AttackMovement>().targetTag = targetTag;
    }

    public void SpawnSlash(Vector3 position, Quaternion rotation)
    {
        networkObject.SendRpc(RPC_SPAWN_SLASH, Receivers.All, position, rotation, team);
    }

    public override void SpawnSlash(RpcArgs args)
    {
        Vector3 position = args.GetNext<Vector3>();
        Quaternion rotation = args.GetNext<Quaternion>();
        int attackTeam = args.GetNext<int>();
        Debug.Log("team " + team);
        Debug.Log("attack " + attackTeam);
        string targetTag = team == attackTeam ? "Enemy" : "Player";
        Instantiate(DoritoAttackPreFab, position, rotation).GetComponent<AttackMovement>().targetTag = targetTag;
    }

    public void PassTurn()
    {
        networkObject.SendRpc(RPC_PASS_TURN, Receivers.Others);
    }

    public override void PassTurn(RpcArgs args)
    {
        this.comboScript.AddCombosCompleted();
        this.comboScript.StartCombo(this.controlScript.selectedWeapon);
    }

    public void TakeDamage()
    {
        this.controlScript.DecreaseHealth();
        networkObject.SendRpc(RPC_TAKE_DAMAGE, Receivers.Others);
    }

    public override void TakeDamage(RpcArgs args)
    {
        this.controlScript.DecreaseOpponentHealth();
    }

    public void YouDied()
    {
        this.controlScript.ShowNoobSign();
        networkObject.SendRpc(RPC_YOU_DIED, Receivers.Others);
    }

    public override void YouDied(RpcArgs args)
    {
        this.controlScript.ShowYeetSign();
    }
}
