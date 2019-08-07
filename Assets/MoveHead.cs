using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveHead : MovementHeadBehavior
{
    private Transform head;
    private Transform leftHand;
    private Transform rightHand;

    // Start is called before the first frame update
    void Start()
    {
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
}
