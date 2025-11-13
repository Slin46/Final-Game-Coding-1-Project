using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //movement variables
    public float walkSpeed = 5f;
    private Rigidbody2D rb2d;
    public float sprintSpeed = 8f;
    private Vector2 moveInput;
    private float inputX;
    private float inputY;
    public bool isSprinting;

    //jump variables
    public float jumpForce = 5f;
    private int jumpCount;
    public int maxJumps = 2;

    //climbing variables
    public float climbSpeed = 3f;
    public LayerMask ladderLayer;
    public Transform ladderCheckPos;
    public float ladderCheckRadius = 0.2f;
    public bool isClimbing;


    //ground variables
    public LayerMask groundLayer;
    public Transform groundCheck;

    //put in game manager
    public List<GameObject> disappearingPlatforms = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            jumpCount = 0;
        }

        Climbing();
    }

    //fixed update is not frame dependent it runs at scheduled intervals
    private void FixedUpdate()
    {
        //local var for current speed
        float currentSpeed;
        //if we are sprinting (pressing left shift) change speed to sprint speed
        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            //if we are not pressing shift change it back to walk speed
            currentSpeed = walkSpeed;
        }
        rb2d.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb2d.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 horizontalInput = context.ReadValue<Vector2>();
        moveInput = new Vector2(horizontalInput.x, 0f);

        if(!isClimbing)
        {
            rb2d.linearVelocity = new Vector2(inputX * walkSpeed, rb2d.linearVelocity.y);
        }
    }

    //bc there is no void it has to return 
    private bool isGrounded()
    {
        //so its making a circle and if our circle touches the ground the player is grounded so it returns true
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }

    //this will draw our overlap circle it is taking the position of ground check and the radius of it
    private void OnDrawGizmos()
    {
        //if we do not have a ground check we are going to exit the function (return);
        if (groundCheck == null) return;
        Gizmos.color = Color.azure;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //this is for if u only want the player to jump once and only when they are on the ground
        /*if (isGrounded())
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }*/

        //if we are pressing space
        if (context.performed)
        {
            //and if our jump count is less than max jumps we can jump
            if (jumpCount < maxJumps)
            {
                //reset the vertical velocity
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                //we are adding 1 to our jump count
                jumpCount++;
            }
        }

    }

    public void Sprint(InputAction.CallbackContext context)
    {
        //if we are pressing the sprint button ("performing it")
        if (context.performed) isSprinting = true;
        //if we are not pressing the sprint buttopn ("canceled")
        if (context.canceled) isSprinting = false;

    }

    public void Climbing()
    {
        //check if inside ladder area using an overlapcircle
        bool onLadder = Physics2D.OverlapCircle(ladderCheckPos.position, ladderCheckRadius, ladderLayer);

        //if its on ladder above .1, player is climbing 
        if (onLadder && Mathf.Abs(inputY) > 0.1f)
        {
            isClimbing = true;
            rb2d.gravityScale = 0f;
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, inputY * climbSpeed);
        }
        else if (isClimbing && !onLadder)
        {
            //leaving the ladder
            isClimbing = false;
            //normal gravity
            rb2d.gravityScale = 4f;
        }
    }
}
