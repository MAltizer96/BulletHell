using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;

    Animator animator;
    SpriteRenderer spriteRenderer;

    Vector2 lastDirection = Vector2.down;
    int lastDirectionValue = -1;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

   

    private void Update()
    {
        Vector2 direction = moveAction.action.ReadValue<Vector2>();

        if (direction.sqrMagnitude > 0.01f)
        {
            int newDirection;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                newDirection = 1;
                spriteRenderer.flipX = direction.x < 0;
            }
            else
            {
                newDirection = direction.y > 0 ? 2 : 0;
            }

            if (newDirection != lastDirectionValue)
            {
                animator.SetInteger("Direction", newDirection);
                lastDirectionValue = newDirection;
            }

            //animator.SetBool("IsMoving", true);
        }
        else
        {
            //animator.SetBool("IsMoving", false);
        }
    }
}