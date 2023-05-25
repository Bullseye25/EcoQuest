using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float underwaterFriction;
    public float gravityMultiplier;
    public float gravityMultiplierUnderWater;
    public float waterGravityMultiplier;

    public bool isUnderwater = false;
    public bool isGrounded = false;
    public bool isOnUnderwaterGround = false;
    public Rigidbody rb;

    public bool isJumping = false;
    public float jumpVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
/*        CalculateJumpVelocity();*/
    }

    void Update()
    {
        float horizontalInput = -Input.GetAxis("Horizontal");
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
}