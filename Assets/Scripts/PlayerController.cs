using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float jumpingPower = 7.5f;
    [SerializeField] private float terminalVelocity = -30;
    [SerializeField] private float fallMultiplier = 4.5f;

    Vector2 vecGravity;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private CapsuleCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("PrincessJump"))
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            }
        }

        //Terminal Velocity aka Fall speed limit
        if (rb.velocity.y < terminalVelocity)
        {
            Vector2 velocityLimit = new Vector2(rb.velocity.x, terminalVelocity);
            rb.velocity = velocityLimit;
        }

        if (rb.velocity.y < 0)
        {
             rb.velocity -= vecGravity * fallMultiplier * 1.5f * Time.deltaTime;  
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        FinalCollisionCheck();
    }

    private void FinalCollisionCheck()
    {
        // Get the velocity
        Vector2 moveDirection = new Vector2(rb.velocity.x * Time.fixedDeltaTime, 0.2f);

        // Get bounds of Collider
        var bottomRight = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.max.y);
        var topLeft = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y);

        // Move collider in direction that we are moving
        bottomRight += moveDirection;
        topLeft += moveDirection;

        // Check if the body's current velocity will result in a collision
        if (Physics2D.OverlapArea(topLeft, bottomRight, groundLayer))
        {
            // If so, stop the movement
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
