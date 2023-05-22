using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    public float underwaterFriction;
    public float gravityMultiplier;
    public float gravityMultiplierUnderWater;
    public float waterGravityMultiplier;
    public Joystick joystick;

    public bool isUnderwater = false;
    public bool isGrounded = false;
    public bool isOnUnderwaterGround = false;
    public Rigidbody rb;
    public ParticleSystem particles;

    public bool isJumping = false;
    public float jumpVelocity;


    public healthManage health;



    public Text timerText;
    public Slider timerSlider;
    public float gameTime = 120f;

    private Timer timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        /*        CalculateJumpVelocity();*/

        timer = new Timer(gameTime);
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        transform.Translate(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

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
            /*if (isUnderwater || isOnUnderwaterGround)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpVelocity / 2f, rb.velocity.z);
            }
            else
            {*/
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            /*}*/
        }

        // Additional code for swimming up
        if (isUnderwater && Input.GetButtonDown("Jump"))
        {
            CalculateJumpVelocity();
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        if (isUnderwater)
        {

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
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer.GetValue() / 60f);
        int seconds = Mathf.FloorToInt(timer.GetValue() % 60f);

        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        Debug.Log(textTime);

        timerText.text = textTime;
        timerSlider.value = timer.GetValue();
    }

    public void jump()
    {
        if ((isGrounded || isUnderwater || isOnUnderwaterGround) && !isJumping)
        {
            CalculateJumpVelocity();
            isJumping = true;
            /*if (isUnderwater || isOnUnderwaterGround)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpVelocity / 2f, rb.velocity.z);
            }
            else
            {*/
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            /*}*/
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            rb.velocity = Vector3.zero; // set velocity to zero
        }
        else if (collision.gameObject.CompareTag("UnderWaterGround"))
        {
            isOnUnderwaterGround = true;
            isGrounded = true;
            isJumping = false;
        }



        // Damage
        if (collision.gameObject.CompareTag("Shark"))
        {
            health.BigDamage();
            particles.Play();
            moveSpeed = 0;
            jumpHeight = 0;
            Invoke("StopParticles", 1f);
        }
        else if (collision.gameObject.CompareTag("Crab"))
        {
            health.SmallDamage();
            particles.Play();
            moveSpeed = 0;
            jumpHeight = 0;
            Invoke("StopParticles", 1f);
        }
        else if (collision.gameObject.CompareTag("SeaHorse"))
        {
            health.BigDamage();
            particles.Play();
            moveSpeed = 0;
            jumpHeight = 0;
            Invoke("StopParticles", 1f);
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

    void StopParticles()
    {
        particles.Stop();
        moveSpeed = 5;
        jumpHeight = 5;
    }

    public void FillSlider()
    {
        timer.Reset(gameTime);
        UpdateTimerUI();
    }
}
