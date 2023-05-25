using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSettings : ControllerCore
{
    protected override void Start()
    {
        base.Start();
        // Add additional initialization code specific to the child class
    }

    protected override void Update()
    {
        base.Update();
        // Add additional update code specific to the child class
    }

    protected override void OnControllerColliderHit(ControllerColliderHit hit)
    {
        base.OnControllerColliderHit(hit);
        // Add additional collider hit logic specific to the child class
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // Add additional trigger enter logic specific to the child class
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        // Add additional trigger exit logic specific to the child class
    }
}

