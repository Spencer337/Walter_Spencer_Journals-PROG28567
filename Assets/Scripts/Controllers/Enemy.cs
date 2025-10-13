using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float stoppingDistance = 1;
    public Vector3 direction;
    public Vector3 velocity;
    public float distance;
    public float maxSpeed = 7;
    public float acceleration;
    public float accelerationTime = 3;
    public float moveInterval = 3;

    public float maxDistance = 20;

    public float frequency = 5f;
    public float amplitude = 5f;
    public Vector3 pos;
    public float moveSpeed = 10;
    public float t;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        pos = transform.position;
    }
    private void Update()
    {

        moveAlongWave();
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

    public void moveAlongWave()
    {
        // Moving horizontally
        pos.x += Time.deltaTime * moveSpeed;

        // Increment t based on time
        t += Time.deltaTime;

        // Moving vertically
        transform.position = pos + transform.up * Mathf.Sin(t * frequency) * amplitude;

        // If at the edge of the screen, invert move speed
        Vector2 cameraPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (cameraPosition.x < 0)
        {
            moveSpeed = moveSpeed * -1;
            pos.x = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        }
        if (cameraPosition.x > Screen.width)
        {
            moveSpeed = moveSpeed * -1;
            Vector3 maximumPoint = new Vector3(Screen.width, Screen.height, 0);
            pos.x = Camera.main.ScreenToWorldPoint(maximumPoint).x;
        }
    }

}
