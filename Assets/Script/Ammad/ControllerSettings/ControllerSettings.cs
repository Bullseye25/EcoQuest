using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerSettings : ControllerCore
{
    private const string BEHAVIOUR = "Anim";

    [Header("Controller Settings")]
    [SerializeField] private Animator behaviour;

    [SerializeField] private UnityEvent onEnterWater = new UnityEvent();
    [SerializeField] private UnityEvent onExitWater = new UnityEvent();

    protected override void Start()
    {
        base.Start();
        // Add additional initialization code specific to the child class
    }

    protected override void Update()
    {
        base.Update();
        // Add additional update code specific to the child class

        JumpAnimationBehaviour();
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

    protected override void HandleWaterEnter()
    {
        base.HandleWaterEnter();

        onEnterWater.Invoke();
    }

    protected override void HandleWaterExit()
    {
        base.HandleWaterExit();

        onExitWater.Invoke();
    }

    private void JumpAnimationBehaviour()
    {

    }

    public void SetAnimation(BotBehaviour value)
    {
        behaviour.SetInteger("BEHAVIOUR", (int)value);
    }

}

public enum BotBehaviour
{
    IDLE = 0,
    WALK = 1,
    JUMP_UP = 2,
    JUMP_Down = 3
}