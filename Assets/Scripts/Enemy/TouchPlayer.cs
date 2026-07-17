using UnityEngine;

public class TouchPlayer : MonoBehaviour
{
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {                 
            Debug.LogError("GameManager not found in the scene.");            
        }
    }

}
