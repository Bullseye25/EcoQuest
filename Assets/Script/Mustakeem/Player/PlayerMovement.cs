using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

     [SerializeField]    private float moveSpeed;
     [SerializeField]    private float jumpHeight;
     [SerializeField]    private float underwaterFriction;
     [SerializeField]    private float gravityMultiplier;
     [SerializeField]    private float gravityMultiplierUnderWater;
     [SerializeField]    private float waterGravityMultiplier;
     [SerializeField]    private Joystick joystick;
     [SerializeField]    private float rotationSpeed = 200f;
     [SerializeField]    private bool isUnderwater = false;
     [SerializeField]    private bool isGrounded = false;
     [SerializeField]    private bool isOnUnderwaterGround = false;
     [SerializeField]    private Rigidbody rb;
     [SerializeField]    private ParticleSystem particles;
     [SerializeField]    private bool isJumping = false;
     [SerializeField]    private float jumpVelocity;
     [SerializeField]    private HealthManage health;
     [SerializeField]    private TextMeshProUGUI timerText;
     [SerializeField]    private Slider timerSlider;
     [SerializeField]    private float gameTime = 120f;
      public Animator animator;
    private PlayerAnimation playerAnimation;

    private bool isWalking = false;
    private bool onGround = true;
    private Timer timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        timer = new Timer(gameTime);
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        playerAnimation = new PlayerAnimation(animator);
    }

    void Update()
    {
        Debug.Log("is walking" + isWalking);
        Debug.Log("is underwater" + isUnderwater);
       // Joystick input
        float joystickInput = -joystick.Horizontal;
        MovePlayer(joystickInput);

        // Keyboard input
        float keyboardInput = -Input.GetAxis("Horizontal");
        MovePlayer(keyboardInput);
         // Set animation states based on player actions
        playerAnimation.UpdateAnimations(isUnderwater, isWalking);

        if (isUnderwater && !isOnUnderwaterGround)
        {
            rb.AddForce(Vector3.down * waterGravityMultiplier * Physics.gravity.magnitude * gravityMultiplierUnderWater, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(Vector3.down * Physics.gravity.magnitude * gravityMultiplier, ForceMode.Acceleration);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && (isGrounded || isUnderwater || isOnUnderwaterGround) && !isJumping)
        {
            CalculateJumpVelocity();
            isJumping = true;
            
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            Jump(playerAnimation);
        }

        // Additional code for swimming up
        if (isUnderwater && Input.GetButtonDown("Jump"))
        {
            CalculateJumpVelocity();
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        if (isUnderwater)
        {
          //  PlayerAnimation.onGround = true;
            timer.Decrement(Time.deltaTime);

            if (timer.HasExpired())
            {
                timer.Stop();
            }

            if (!timer.IsStopped())
            {
                UpdateTimerUI();
            }
        }
        if (!isUnderwater)
        {
            if (CollectibleSystem.cylinder)
            {
                timer.Increment(Time.deltaTime * 10);
            }
            else
            {
                timer.Increment(Time.deltaTime);
            }


            if (timer.HasReached(gameTime))
            {
                timer.Reset(gameTime);
            }

            UpdateTimerUI();
        }
    }
    private void Jump(PlayerAnimation playerAnimation)
    {
        if (onGround)
        {
            playerAnimation.TriggerJump();
        }
    }
     private void MovePlayer(float input)
    {
        isWalking = true;
       // Calculate movement direction
        Vector3 movement = new Vector3(input, 0f, 0f);

        // Move the player
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // if (movement.magnitude > 0f)
        // {
        //     Quaternion toRotation = Quaternion.LookRotation(movement, Vector2.up);
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }
    }
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer.GetValue() / 60f);
        int seconds = Mathf.FloorToInt(timer.GetValue() % 60f);

        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        

        timerText.text = textTime;
        timerSlider.value = timer.GetValue();
    }

    public void jump()
    {
        if ((isGrounded || isUnderwater || isOnUnderwaterGround) && !isJumping)
        {
            CalculateJumpVelocity();
            isJumping = true;
         
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        // Additional code for swimming up
        if (isUnderwater)
        {
            CalculateJumpVelocity();
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
    }


    void OnCollisionEnter(Collision collision)
{
    switch (collision.gameObject.tag)
    {
        case "Ground":
            isGrounded = true;
            isJumping = false;
            rb.velocity = Vector3.zero; // set velocity to zero
            break;
        case "UnderWaterGround":
            isOnUnderwaterGround = true;
            isGrounded = true;
            isJumping = false;
            break;
        case "Shark":
            health.BigDamage();
            particles.Play();
            moveSpeed = 0;
            jumpHeight = 0;
            StartCoroutine(StopParticles());
            break;
        case "Crab":
            health.SmallDamage();
            particles.Play();
            moveSpeed = 0;
            jumpHeight = 0;
           StartCoroutine(StopParticles());;
            break;
        case "SeaHorse":
            health.BigDamage();
            particles.Play();
            moveSpeed = 0;
            jumpHeight = 0;
            StartCoroutine(StopParticles());
            break;
        default:
            break;
    }
}

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("UnderWaterGround"))
        {
            isOnUnderwaterGround = false;
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isUnderwater = true;
            rb.drag = underwaterFriction;
            rb.useGravity = true;
        }
        else if (other.CompareTag("UnderWaterGround"))
        {
            isOnUnderwaterGround = true;
            isUnderwater = true;
            isGrounded = true;
            rb.drag = underwaterFriction;
            rb.useGravity = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isUnderwater = false;
            isGrounded = false;
            isOnUnderwaterGround = false;
            rb.drag = 0;
            rb.useGravity = false;
        }
        else if (other.CompareTag("UnderWaterGround"))
        {
            isOnUnderwaterGround = false;
            isGrounded = false;
        }
    }

    void CalculateJumpVelocity()
    {
        float g = Physics.gravity.magnitude * gravityMultiplier;
        jumpVelocity = Mathf.Sqrt(2 * g * (isUnderwater == true ? jumpHeight : jumpHeight / 2));
    }
    public void FillSlider()
    {
        timer.Reset(gameTime);
        UpdateTimerUI();
    }

   IEnumerator StopParticles()
    {
        // Coroutine logic
        

        yield return new WaitForSeconds(1f);

         particles.Stop();
        moveSpeed = 5;
        jumpHeight = 5;
    }
}
