using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerCore : MonoBehaviour
{
    [Header("Controller Core")]

    [SerializeField] protected CharacterController characterController;

    [Space]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpHeight;
    [SerializeField] protected float gravityMultiplier;
    [SerializeField] protected LayerMask ignoredLayers; // Array of layers to be ignored by CharacterController

    [Space]
    [Header("Water Settings")]
    [SerializeField] protected float underwaterFriction;
    [SerializeField] protected float gravityMultiplierUnderWater;
    [SerializeField] protected float waterGravityMultiplier;

    [Space]
    [SerializeField] protected bool isUnderwater = false;
    [SerializeField] protected bool isGrounded = false;
    [SerializeField] protected bool isOnUnderwaterGround = false;

    [Space]
    [SerializeField] protected UnityEvent onJump = new UnityEvent();

    [Header("Do Not Modify Following")]
    [SerializeField] protected bool isJumping = false;
    [SerializeField] protected float jumpVelocity;
    [SerializeField] protected float horizontalInput;
    [SerializeField] protected float gravity;
    [SerializeField] protected float deltaTime;

    protected Vector3 moveDirection;

    [SerializeField] private Transform bot;
    [SerializeField] private Animator behaviour;
    private const string BEHAVIOUR = "Anim";
    private Vector3 previousDirection;

    private bool jumpAct = false;

    public void InputAct(Vector2 inputAccess)
    {
        horizontalInput = -inputAccess.normalized.x;
    }

    public void JumpAct(bool value)
    {
        jumpAct = value;
    }

    protected virtual void Start()
    {
        CalculateJumpVelocity();
        InitializeCharacterController();
    }

    protected virtual void Update()
    {
        deltaTime = Time.deltaTime;
/*        horizontalInput = -Input.GetAxis("Horizontal");*/
        moveDirection = new Vector3(horizontalInput * moveSpeed, moveDirection.y, 0);

        LocomotionBehaviour();

        gravity = Physics.gravity.y * deltaTime;

        if (isUnderwater && !isOnUnderwaterGround)
        {
            moveDirection.y += gravity * waterGravityMultiplier * gravityMultiplierUnderWater;
        }
        else
        {
            moveDirection.y += gravity * gravityMultiplier;
        }

        HandleJump();

        characterController.Move(moveDirection * deltaTime);

        var position = transform.position;
        transform.position = new Vector3(position.x, position.y, -3f);

        HandleAnimationState();
    }

    private void HandleAnimationState()
    {
        if (horizontalInput != 0)
        {
            if (isGrounded && !isJumping)
                SetAnimation(BotBehaviour.WALK);
            else if (isJumping && isUnderwater && !isGrounded && !isOnUnderwaterGround)
                SetAnimation(BotBehaviour.SWIM);
            else if (!isGrounded && isUnderwater && !isJumping)
                SetAnimation(BotBehaviour.SWIM);
            else if (!isGrounded && !isUnderwater && !isOnUnderwaterGround && isJumping)
                SetAnimation(BotBehaviour.JUMP_UP);
            else if (!isGrounded && isUnderwater && isOnUnderwaterGround && isJumping)
                SetAnimation(BotBehaviour.SWIM);
        }
        else
        {
            if (isGrounded && !isJumping)
                SetAnimation(BotBehaviour.IDLE);
            else if (isJumping && isUnderwater && !isGrounded && !isOnUnderwaterGround)
                SetAnimation(BotBehaviour.SWIM);
            else if (!isGrounded && isUnderwater && !isJumping)
                SetAnimation(BotBehaviour.SWIM);
            else if (!isGrounded && !isUnderwater && !isOnUnderwaterGround && isJumping)
                SetAnimation(BotBehaviour.JUMP_UP);
            else if (!isGrounded && isUnderwater && isOnUnderwaterGround && isJumping)
                SetAnimation(BotBehaviour.SWIM);
        }
    }

    private void LocomotionBehaviour()
    {
        Vector3 currentDirection = Vector3.zero;
        if (horizontalInput < -0.01f)
        {
            currentDirection = Vector3.left;
        }
        else if (horizontalInput > 0.01f)
        {
            currentDirection = Vector3.right;
        }

        // Set the rotation based on the previous movement direction
        if (currentDirection != Vector3.zero)
        {
            previousDirection = currentDirection;
        }

        if (previousDirection == Vector3.left)
        {
            bot.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (previousDirection == Vector3.right)
        {
            bot.rotation = Quaternion.Euler(0, 90, 0);
        }

    }

    protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (((1 << hit.gameObject.layer) & ignoredLayers) != 0)
        {
            return; // Ignore collision with specified layers
        }

        if (hit.gameObject.CompareTag("Ground"))
        {
            HandleGroundCollision();
        }
        else if (hit.gameObject.CompareTag("UnderWaterGround"))
        {
            HandleUnderwaterGroundCollision();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            HandleWaterEnter();
        }
        else if (other.CompareTag("UnderWaterGround"))
        {
            HandleUnderwaterGroundEnter();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            HandleWaterExit();
        }
        else if (other.CompareTag("UnderWaterGround"))
        {
            HandleUnderwaterGroundExit();
        }
    }

    protected virtual void CalculateJumpVelocity()
    {
        float g = Physics.gravity.magnitude * (isUnderwater ? gravityMultiplierUnderWater : gravityMultiplier);
        jumpVelocity = Mathf.Sqrt(2 * g * (isUnderwater ? jumpHeight : jumpHeight / 2));
    }

    protected virtual void HandleJump()
    {
        if (jumpAct/*Input.GetButtonDown("Jump")*/ && (isGrounded || isUnderwater || isOnUnderwaterGround) && !isJumping)
        {
            isJumping = true;
            isGrounded = false;
            moveDirection.y = jumpVelocity;
            onJump.Invoke();
        }

        if (isUnderwater && jumpAct/*Input.GetButtonDown("Jump")*/)
        {
            moveDirection.y = jumpVelocity;
            onJump.Invoke();
        }
    }

    protected virtual void HandleGroundCollision()
    {
        isGrounded = true;
        isJumping = false;

        moveDirection.y = 0;
    }

    protected virtual void HandleUnderwaterGroundCollision()
    {
        isOnUnderwaterGround = true;
        isGrounded = true;
        isJumping = false;
    }

    protected virtual void HandleWaterEnter()
    {
        isUnderwater = true;

        CalculateJumpVelocity();
    }

    protected virtual void HandleUnderwaterGroundEnter()
    {
        isOnUnderwaterGround = true;
        isUnderwater = true;
        isGrounded = true;
    }

    protected virtual void HandleWaterExit()
    {
        isUnderwater = false;
        isGrounded = false;
        isOnUnderwaterGround = false;
        CalculateJumpVelocity();
    }

    protected virtual void HandleUnderwaterGroundExit()
    {
        isOnUnderwaterGround = false;
        isGrounded = false;
    }

    protected virtual void InitializeCharacterController()
    {
        // Add initialization code for the character controller
    }

    public void SetAnimation(BotBehaviour value)
    {
        if (value == BotBehaviour.IDLE)
        {
            behaviour.SetInteger(BEHAVIOUR, 0);
        }

        if (value == BotBehaviour.WALK)
        {
            behaviour.SetInteger(BEHAVIOUR, 1);
        }

        if (value == BotBehaviour.SWIM)
        {
            behaviour.SetInteger(BEHAVIOUR, 2);

        }

        if (value == BotBehaviour.JUMP_UP)
        {
            behaviour.SetInteger(BEHAVIOUR, 3);

        }

        if (value == BotBehaviour.JUMP_Down)
        {
            behaviour.SetInteger(BEHAVIOUR, 4);

        }
    }

    public void SetAnimation(int value)
    {
        behaviour.SetInteger(BEHAVIOUR, value);
    }
}


public enum BotBehaviour
{
    IDLE,
    WALK,
    SWIM,
    JUMP_UP,
    JUMP_Down
}