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
        if (collision.tag == "Player")
        {
            return;
        }
        //Debug.Log("Collision: " + collision.name);
        iEnemy damageable = collision.GetComponent<iEnemy>();
        var damGO = damageable as Behaviour;
        //if (damGO.gameObject != collision.gameObject) 
        //{
        //    return;
        //}
        //Debug.Log("Damageable: " + damageable);
        if (damageable != null)
        {

            if(damGO.gameObject != collision.gameObject)
            {
                return;
            }
            //Debug.Log("Bullet hit damageable: " + collision.gameObject.name);
            Vector2 knockBackDirection = rb.linearVelocity.normalized;
            float knockBackForce = KnockBack;

            damageable.TakeDamage();
            damageable.KnockBack(damageable,rb.linearVelocity.normalized, knockBackForce);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }
}
