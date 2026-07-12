using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    private float knockBack;
    private int bulletDamage;

    public float KnockBack { get => knockBack; set => knockBack = value; }
    public int BulletDamage { get => bulletDamage; set => bulletDamage = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        iDamageable damageable = collision.GetComponent<iDamageable>();
        if (damageable != null)
        {
            Debug.Log("Bullet hit damageable: " + collision.gameObject.name);
            Vector2 knockBackDirection = rb.linearVelocity.normalized;
            float knockBackForce = KnockBack;

            damageable.TakeDamage(BulletDamage);
            damageable.KnockBack(rb.linearVelocity.normalized, knockBackForce);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }
}
