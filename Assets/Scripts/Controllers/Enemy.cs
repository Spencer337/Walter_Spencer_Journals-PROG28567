using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float stoppingDistance = 2;
    public Vector3 direction;
    public Vector3 velocity;
    public float distance;
    public float maxSpeed = 7;
    public float acceleration;
    public float accelerationTime = 3;
    public float moveInterval = 3;
    public float t;
    public float maxDistance = 20;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
    }
    private void Update()
    {
        t += Time.deltaTime; 
        if (t >= moveInterval)
        {
            moveTowardPlayer();
        }
        
    }

    public void moveTowardPlayer()
    {
        direction = (playerTransform.position - transform.position).normalized;
        velocity += (Vector3)direction * acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance <= stoppingDistance)
        {
            direction = Vector3.zero;
            velocity = Vector3.zero;
            t = 0;
        }

        // If the enemy gets too far away from the player, add some deceleration to make movements tighter
        if (distance >= maxDistance)
        {
            velocity -= velocity * acceleration * Time.deltaTime;
        }

    }

}
