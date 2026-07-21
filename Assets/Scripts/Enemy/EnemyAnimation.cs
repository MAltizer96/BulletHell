using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    Transform player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {

        Vector2 direction = player.position - transform.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Side movement
            animator.SetInteger("Direction", 1);

            // Flip sprite
            spriteRenderer.flipX = direction.x < 0;
        }
        else
        {
            if (direction.y > 0)
            {
                // Player is above
                animator.SetInteger("Direction", 2);
            }
            else
            {
                // Player is below
                animator.SetInteger("Direction", 0);
            }
        }
    }

}
