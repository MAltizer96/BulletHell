using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 movementInput;
    private Rigidbody2D rb;

    bool beingKnockedback = false;
    [SerializeField]
    float baseKnockBackCooldown; // Cooldown duration in seconds
    float knockBackCooldown; // Current cooldown timer

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // This is called by the PlayerInput component only when the keys change
    private void OnMove(InputValue value)
    {
        // Store the Vector2 (e.g., (1, 0) for Right, (0, 0) for None)
        movementInput = value.Get<Vector2>();
        //Debug.Log("Input Updated: " + movementInput);
    }

    private void Update()
    {
        if (knockBackCooldown > 0)
        {
            knockBackCooldown -= Time.deltaTime;
        }
        if (knockBackCooldown <= 0)
        {
            beingKnockedback = false;

        }
    }
    private void FixedUpdate()
    {
        if(beingKnockedback)
        {
            return;
        }
        Vector2 moveDirection = new Vector3(movementInput.x, movementInput.y);
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        //transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void knockBack(Vector2 direction)
    {
        if (beingKnockedback)
        {
            return;
        }
        rb.linearVelocity = Vector2.zero; // clear existing momentum first
        rb.AddForce(direction, ForceMode2D.Impulse);
        beingKnockedback = true;
        knockBackCooldown = baseKnockBackCooldown;

        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage();
        }

    }

}
