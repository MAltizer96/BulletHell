using UnityEngine;
using Pathfinding;
using System.Collections;
public class EnemyMovement : MonoBehaviour
{
    private AILerp aiLerp;
    private bool isKnocked;

    private Transform playerTransform;


    [SerializeField]
    float speed;


    private void OnEnable()
    {
        EnemyEvents.OnKnockBack += KnockBack;
    }
    private void OnDisable()
    {
        EnemyEvents.OnKnockBack -= KnockBack;
    }
    private void Start()
    {
        aiLerp = GetComponent<AILerp>();


        //rb = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        aiLerp.speed = speed;
    }
    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            //Debug.Log("Moving");
            aiLerp.destination = playerTransform.position;

        }
    }
    public void KnockBack(iEnemy enemy, Vector2 direction, float force)
    {
         
        StartCoroutine(KnockBackRoutine(enemy, direction, force));
    }
    private IEnumerator KnockBackRoutine(iEnemy enemy,Vector2 direction, float force)
    {
        // Disable AILerp so it stops fighting the knockback
        var enemyBehavior = enemy as Behaviour;
        GameObject enemyGO = enemyBehavior.gameObject;
        Rigidbody2D rb = enemyGO.GetComponent<Rigidbody2D>();

        AILerp enemyLerp = enemyGO.GetComponent<AILerp>();
        enemyLerp.enabled = false;

        isKnocked = true;

        rb.linearVelocity = direction * force;

        float knockBackTime = 0.2f;
        // Wait for knockback to finish
        yield return new WaitForSeconds(knockBackTime);
        isKnocked = false;
        // Re-enable AILerp
        aiLerp.enabled = true;
    }
}
