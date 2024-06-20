using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public bool canMove = true;
    private float horizontal;
    public ScoreManager scoreManager;

    private bool isFacingRight = true;
    public OrbAttack orbAttack;
    public float moveSpeed;
    public float walkSpeed = 2f; // Default walking speed
    public float collisionOffset = .05f;
    private ContactFilter2D movementFilter; // Correctly typed and instantiated
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // Fixed semicolon
    Animator animator;

    public float health = 10;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
            else
            {
                StartCoroutine(Shake(0.15f, 0.1f));
            }
        }
        get
        {
            return health;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Defeated()
    {
        animator.SetTrigger("isDefeated");
    }

    public void RemoveEnemy()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(1);
        }
        else
        {
            Debug.LogError("ScoreManager reference is not set in the Inspector.");
        }
        Destroy(gameObject);
    }

    void Update()
    {
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
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void OrbAttack()
    {
        LockMovement();
        if (isFacingRight)
        {
            orbAttack.AttackRight();
        }
        else
        {
            orbAttack.AttackLeft();
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    // This method should be called by an animation event at the end of the death animation
    public void OnDefeatedAnimationEnd()
    {
        RemoveEnemy();
    }
}
