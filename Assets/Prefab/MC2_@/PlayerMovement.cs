using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects; //Ground Objects
    public float checkRadius;

    private bool allowedMoving = true;

    private bool isJumping = false;
    public Rigidbody2D rigidBody2D;
    private Vector2 moveVelocity;
    private float moveInput;
    private bool facingRight = false;
    private bool isGrounded;
    [SerializeField] private GameObject sprite;
    public LayerMask playerLayer;
    public LayerMask npcLayer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();

        // Ignore NPC Collision
        Physics2D.IgnoreLayerCollision( 8,9, true);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        rigidBody2D.velocity = new Vector2(moveInput*speed, rigidBody2D.velocity.y);
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
        Animate();
        animator.SetFloat("xVelocity", Mathf.Abs(moveInput));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
        }

        if(context.canceled && rigidBody2D.velocity.y > 0f)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y * 0.5f);
        }
    }

    private void Animate()
    {
        if (moveInput > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        sprite.transform.Rotate(0f, 180f, 0f);
    }

    public void DisableMovement(){
            allowedMoving = false;
    }

    public void EnableMovement(){
        allowedMoving = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
    }
    
}
