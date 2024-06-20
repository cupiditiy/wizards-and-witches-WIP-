using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    public WhipAttack whipAttack;
    bool canMove = true;
    public float moveSpeed;
    public float walkSpeed = 2f; // Default walking speed
    public float runSpeed = 5f; // Default running speed
    public float jumpForce = 5f;
    public float collisionOffset = .05f;
    private ContactFilter2D movementFilter; // Correctly typed and instantiated
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // Fixed semicolon
    public Collider2D playerCollider;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Initialize the movement filter here, if needed, for example:
        // movementFilter.useTriggers = false;
        // You can adjust the filter settings according to your needs
        animator = GetComponent<Animator>();
    }

 void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        // Flip character sprite based on movement direction
        Flip();
    }
       private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FixedUpdate()
    {

        if (canMove)
        {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                bool isRunning = (horizontal != 0) &&
                             (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
                moveSpeed = isRunning ? runSpeed : walkSpeed;

                if (isRunning)
                {
                    animator.SetBool("isRunning", success);

                }
                moveSpeed = isRunning ? runSpeed : walkSpeed;
                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isRunning", false);
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else { return false; 
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("whipAttack");
     }

    public void WhipAttack() {
        LockMovement();
        if (isFacingRight)
        {
            whipAttack.AttackRight();
        }
        else { 
            whipAttack.AttackLeft();
        }
        
    }
    public void stopWhipAttack() {
        UnlockMovement();
        whipAttack.StopAttack();
    }

    public void LockMovement() {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }

}
