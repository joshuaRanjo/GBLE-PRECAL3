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
    [SerializeField] private PhysicsMaterial2D someFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    
    

    private bool allowedMoving = true;
    private bool isJumping = false;
    private bool facingRight = false;
    public bool isGrounded;
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

        if(rigidBody2D.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
        
        if(IsGrounded() && !isOnSlope && !isJumping)
        {
            rigidBody2D.velocity = new Vector2(moveInput*speed, 0.0f);
        }
        else if(IsGrounded() && isOnSlope && !isJumping)
        {
            rigidBody2D.velocity = new Vector2(slopeNormalPerpendicular.x * speed * -moveInput, speed * slopeNormalPerpendicular.y * -moveInput);
        }
        else if(!IsGrounded())
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
             Debug.Log("Jumps");
            //Vector2 newforce.Set(0.0f, jumpForce);
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            isJumping = true;
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
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0f, (colliderSize.y / 2) -0.4f);

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
        Vector2 check2 = checkPos - new Vector2(-0.1f,0f);
        Vector2 check3 = checkPos - new Vector2(0.1f,0f);

        RaycastHit2D hit2 = Physics2D.Raycast(check2, Vector2.down, slopeCheckDistance, groundObjects);
        RaycastHit2D hit3 = Physics2D.Raycast(check3, Vector2.down, slopeCheckDistance, groundObjects);
        
        int numHits = 0;
        if(hit)
        {
            numHits++;
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
           ;
            if(slopeDownAngle > 0f)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal,Color.green);
            
        }
        if(hit2)
        {
            numHits++;
            slopeNormalPerpendicular = Vector2.Perpendicular(hit2.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit2.point, slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit2.point, hit2.normal,Color.green);
        }
        if(hit3)
        {
            numHits++;
            slopeNormalPerpendicular = Vector2.Perpendicular(hit3.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit3.point, slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit3.point, hit3.normal,Color.green);
        }

        if(isOnSlope && moveInput == 0.0f)
        {
            
            rigidBody2D.sharedMaterial = fullFriction;
            if(slopeDownAngle > 45f || numHits < 3)
            {
                 rigidBody2D.sharedMaterial = someFriction;
            }

        }
        else
        {
            rigidBody2D.sharedMaterial = noFriction;
        }
        
    }


    
}

