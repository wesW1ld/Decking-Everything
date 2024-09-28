using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //horizontal movement
    public float speed; //can be changed in unity editor
    private float move;
    private Rigidbody2D rb;

    //jumping
    public bool isGrounded;
    public float jumpForce; //can be changed in unity editor
    private Vector2 rayStart;
    public float groundCheckDistance = 0.1f; //distance to ground to check for jumping
    public LayerMask groundLayer; //set ground to have "floor" layer in unity
    private Boolean jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        //player jumping using raycast
        Debug.DrawRay(rayStart, Vector2.down * groundCheckDistance, Color.red); //draws a ray in the scene view for debugging
        rayStart = new Vector2(transform.position.x, transform.position.y - 0.5f);
        isGrounded = Physics2D.Raycast(rayStart, Vector2.down, groundCheckDistance, groundLayer);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
    }

    //FixedUpdate is called at a fixed interval and is used for physics calculations
    private void FixedUpdate() 
    {
        ///player movement
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if(jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }
    }
}
