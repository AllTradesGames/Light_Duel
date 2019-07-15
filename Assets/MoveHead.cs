using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class MoveHead : MovementHeadBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject != null)
        {
            networkObject.Position = transform.position;
            networkObject.Rotation = transform.rotation;
        }
    }
}
