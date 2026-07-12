using UnityEngine;
using Pathfinding;
public class EnemyMovement : MonoBehaviour
{
    private AILerp aiLerp;

    private Transform playerTransform;
    private void Start()
    {
        aiLerp = GetComponent<AILerp>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        if (playerTransform != null)
        {

            aiLerp.destination = playerTransform.position;
            //Debug.Log("Has Path: " + aiLerp.hasPath);
            //Debug.Log("Remaining Distance: " + aiLerp.remainingDistance);
            //Debug.Log("Destination: " + aiLerp.destination);

        }
    }

}
