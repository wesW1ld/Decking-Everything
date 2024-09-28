using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //horizontal movement
    public float speed; //can be changed in unity editor
    private float horizontalMove;
    private Rigidbody2D rb;

    //jumping
    public bool isGrounded;
    public float jumpForce; //can be changed in unity editor
    private Vector2 rayStart;
    public float groundCheckDistance = 0.1f; //distance to ground to check for jumping
    public LayerMask groundLayer; //set ground to have "floor" layer in unity

    // Quality of life improvements for jumping that give the player a bit more room for error when trying to jump.
    [Header("Jump QoL")]
    private float coyoteTime = 0.15f; // Time player has to jump after falling off a platform.
    private float coyoteTimeCounter;
    private float jumpBufferTime = 1.15f; // Time before hitting the ground that the player can jump.
    private float jumpBufferCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = InputManager.instance.moveInput.x;

        //player jumping using raycast
        Debug.DrawRay(rayStart, Vector2.down * groundCheckDistance, Color.red); //draws a ray in the scene view for debugging
        rayStart = new Vector2(transform.position.x, transform.position.y - 1f);
        isGrounded = Physics2D.Raycast(rayStart, Vector2.down, groundCheckDistance, groundLayer);

        // Function that applies coyote time and a jump buffer to the player's jump.
        CoyoteTimeAndBuffer();
    }

    //FixedUpdate is called at a fixed interval and is used for physics calculations
    private void FixedUpdate() 
    {
        //sprite movement
        if(horizontalMove < 0)
        {
            transform.localScale = new Vector3(.5f, .5f, 1f);
        }
        else if(horizontalMove > 0)
        {
            transform.localScale = new Vector3(-.5f, .5f, 1f);
        }

        ///player movement
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        //JUMP!!!
        Jump();
    }

    private void Jump()
    {
        // As long as the coyote timer and jump buffer are greater than 0, the player can jump.
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
        
    }

    private void CoyoteTimeAndBuffer()
    {
        // Coyote time.
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        // Coyote timer does not count down until the player leaves the ground.
        // If jump is pressed before the counter reaches 0, the player jumps.
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // When jump is pressed, the counter is set.
        if (InputManager.instance.jumpInput)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        // The counter counts down. If the player hits the ground before the counter reaches 0, the player jumps.
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
}
