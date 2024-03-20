using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundObjects; //Ground Objects
    [SerializeField] private float checkRadius;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private GameObject sprite;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    
    




    private bool allowedMoving = true;
    private bool isJumping = false;
    private bool facingRight = false;
    private bool isGrounded;
    private bool isOnSlope;

    private float moveInput;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerpendicular;


    public LayerMask playerLayer;
    public LayerMask npcLayer;


    private Rigidbody2D rigidBody2D;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = sprite.GetComponent<Animator>();

        colliderSize = capsuleCollider.size;

        // Ignore NPC Collision
        Physics2D.IgnoreLayerCollision( 8,9, true);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
        SlopeCheck();
    }

    private void ApplyMovement()
    {
        rigidBody2D.velocity = new Vector2(moveInput*speed, rigidBody2D.velocity.y);
        
        if(isGrounded && !isOnSlope)
        {
            rigidBody2D.velocity = new Vector2(moveInput*speed, 0.0f);
        }
        else if(isGrounded && isOnSlope)
        {
            rigidBody2D.velocity = new Vector2(slopeNormalPerpendicular.x * speed * -moveInput, rigidBody2D.velocity.y);
        }
        else if(!isGrounded)
        {
            rigidBody2D.velocity = new Vector2(moveInput*speed, rigidBody2D.velocity.y);
        }
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

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D frontHit = Physics2D.Raycast(checkPos, Vector2.left, slopeCheckDistance, groundObjects);
        RaycastHit2D backHit = Physics2D.Raycast(checkPos, -Vector2.left, slopeCheckDistance, groundObjects);

        if(frontHit)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(frontHit.normal, Vector2.up);
        }
        else if(backHit)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(backHit.normal, Vector2.up);
        }
        else
        {
            isOnSlope = false;
            slopeSideAngle = 0.0f;
        }
    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundObjects);

        if(hit)
        {
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal,Color.green);
        }

        if(isOnSlope && moveInput == 0.0f)
        {
            rigidBody2D.sharedMaterial = fullFriction;
        }
        else{
            rigidBody2D.sharedMaterial = noFriction;
        }
    }
    
}
