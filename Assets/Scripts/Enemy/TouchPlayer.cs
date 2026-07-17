using System.Collections;
using UnityEngine;

public class TouchPlayer : MonoBehaviour
{

    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    float baseHitCooldown; // Cooldown duration in seconds
    float hitCooldown; // Current cooldown timer

    BoxCollider2D boxCollider;
    GameObject playerGameObject;

    bool hitPlayer = false; // Flag to track if the player has been hit
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D component is missing on " + gameObject.name);
        }
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (hitPlayer)
        {
            hitCooldown -= Time.deltaTime;
            if (hitCooldown <= 0f)
            {
                hitPlayer = false;
                hitCooldown = baseHitCooldown; 
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hitPlayer)
        {                 
            Debug.Log(gameObject.name + " Touched Player");   
            hitPlayer = true;

            Vector2 knockDirection = (collision.transform.position - transform.position).normalized;

            var rb = collision.GetComponent<Rigidbody2D>();
            StartCoroutine(StopPlayerMovement());
            if (rb != null)
            {
                // Use impulse so it feels like a single knock
                rb.linearVelocity = Vector2.zero; // clear existing momentum first
                rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);
                Debug.Log("Knockback applied to player with force: " + knockDirection * knockbackForce);
            }
        }
    }

    private IEnumerator StopPlayerMovement()
    {
        // Disable player movement
        var playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            Debug.Log("Player movement disabled.");
        }
        // Wait for a short duration
        yield return new WaitForSeconds(hitCooldown);
        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            Debug.Log("Player movement re-enabled.");
        }

   

    }

}
