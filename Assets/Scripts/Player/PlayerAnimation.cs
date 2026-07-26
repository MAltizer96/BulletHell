using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float deadzone = 0.01f;

    Animator animator;
    SpriteRenderer spriteRenderer;

    int lastDirectionValue = -1;
    bool lastIsMoving = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Optional: initialize animator params to a sensible default (facing down, idle)
        if (animator != null)
        {
            animator.SetInteger("Direction", 0);
            animator.SetBool("IsMoving", false);
        }
        lastDirectionValue = 0;
        lastIsMoving = false;
    }

    private void OnEnable()
    {
        moveAction?.action?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action?.Disable();
    }

    private void Update()
    {
        Vector2 direction = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        bool isMoving = direction.sqrMagnitude > (deadzone * deadzone);

        if (isMoving)
        {
            int newDirection;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // horizontal
                newDirection = 1;
                if (spriteRenderer != null) spriteRenderer.flipX = direction.x < 0;
            }
            else
            {
                // vertical
                newDirection = direction.y > 0 ? 2 : 0;
            }

            if (animator != null && newDirection != lastDirectionValue)
            {
                animator.SetInteger("Direction", newDirection);
                lastDirectionValue = newDirection;
            }
        }

        // Update IsMoving only when it changes to reduce animator churn
        if (animator != null && isMoving != lastIsMoving)
        {
            animator.SetBool("IsMoving", isMoving);
            lastIsMoving = isMoving;
        }
    }
}