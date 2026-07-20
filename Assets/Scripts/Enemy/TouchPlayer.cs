using System.Collections;
using UnityEngine;

public class TouchPlayer : MonoBehaviour
{

    [SerializeField]
    private float knockbackForce;

    PlayerMovement playerMovement;
    BoxCollider2D boxCollider;
    GameObject playerGameObject;
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D component is missing on " + gameObject.name);
        }
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerGameObject.GetComponent<PlayerMovement>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {                 
            Debug.Log(gameObject.name + " Touched Player");   
            Vector2 knockDirection = (collision.transform.position - transform.position).normalized;

            var rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = knockDirection * knockbackForce;
                playerMovement.knockBack(direction);
            }
        }
    }



}
