using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    public Vector2 offset = new Vector2(0,1);
    public float warpSpeed = 0.5f;
    public float maxSpeed = 5;
    public float acceleration;
    public float accelerationTime = 2;
    public float bombSpacing = -0.2f;
    public int numberOfTrailBombs = 3;
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 direction = Vector3.zero;
    public float timer;
    public Vector3 deceleration;
    public float decelerationTime = 1;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
    }
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.W))
        {
            WarpPlayer(enemyTransform, warpSpeed);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(offset);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnBombTrail(bombSpacing, numberOfTrailBombs);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnBombOnRandomCorner(1.5f);
        }
        if (Input.GetKey(KeyCode.R))
        {
            DetectAsteroids(5f, asteroidTransforms);
        }
    }

    public void PlayerMovement()
    {   
        // Move the object with acceleration logic
        // When right key is pressed, increase the direction to the right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        // When left key is pressed, increase the direction to the left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        // When up key is pressed, increase the direction upwards
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.up;
        }
        // When down key is pressed, increase the direction downwards
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.down;
        }
        // Normalize direction vector
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            direction = direction.normalized;
            velocity += (Vector3)direction * acceleration * Time.deltaTime;
        }

        // Make sure velocity is not over max speed, and set it to max speed if it is
        if (velocity.x > maxSpeed)
        {
            velocity.x = maxSpeed;
        }
        if (velocity.x < -maxSpeed)
        {
            velocity.x = -maxSpeed;
        }
        if (velocity.y > maxSpeed)
        {
            velocity.y = maxSpeed;
        }
        if (velocity.y < -maxSpeed)
        {
            velocity.y = -maxSpeed;
        }

        transform.position += velocity * Time.deltaTime;


        //Somewhere in your code, you will subtract the deceleration
        //When you stop pressing input
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            deceleration.x = velocity.x / decelerationTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            deceleration.x = velocity.x / decelerationTime;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            deceleration.y = velocity.y / decelerationTime;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            deceleration.y = velocity.y / decelerationTime;
        }

        velocity -= deceleration * Time.deltaTime;

        // If player was moving right before decelerating
        if (deceleration.x > 0 )
        {
            if (velocity.x <= 0)
            {
                deceleration.x = 0;
                velocity.x = 0;
            }
        }
        // If player was moving left before decelerating
        if (deceleration.x < 0)
        {
            if (velocity.x >= 0)
            {
                deceleration.x = 0;
                velocity.x = 0;
            }
        }
        // If player was moving up before decelerating
        if (deceleration.y > 0)
        {
            if (velocity.y <= 0)
            {
                deceleration.y = 0;
                velocity.y = 0;
            }
        }
        // If player was moving down before decelerating
        if (deceleration.y < 0)
        {
            if (velocity.y >= 0)
            {
                deceleration.y = 0;
                velocity.y = 0;
            }
        }
    }

    public void SpawnBombAtOffset(Vector2 inOffset)
    {
        Vector2 spawnPosition = (Vector2)transform.position + inOffset;
        GameObject newBomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }

    public void WarpPlayer (Transform target, float ratio)
    {
        transform.position = Vector2.Lerp(transform.position, target.position, ratio);
    }

    public void SpawnBombTrail (float bombSpacing, int numberOfTrailBombs)
    {
        Vector2 spawnPosition = transform.position;
        for (int i = 0; i < numberOfTrailBombs; i++)
        {
            spawnPosition.y += bombSpacing;
            GameObject newBomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void SpawnBombOnRandomCorner (float inDistance)
    {
        GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        newBomb.transform.Rotate(0, 0, 45);
        int direction = Random.Range(0, 4);
        // Spawns a bomb to the top left of the player
        if (direction == 0)
        {
            newBomb.transform.position += newBomb.transform.up * inDistance;
        }
        // Spawns a bomb to the top right of the player
        else if (direction == 1)
        {
            newBomb.transform.position += newBomb.transform.right * inDistance;
        }
        // Spawns a bomb to the bottom right of the player
        else if (direction == 2)
        {
            newBomb.transform.position += newBomb.transform.up * -inDistance;
        }
        // Spawns a bomb to the bottom left of the player
        else if (direction == 3)
        {
            newBomb.transform.position += newBomb.transform.right * -inDistance;
        }
    }

    public void DetectAsteroids(float inMaxRange, List <Transform> inAsteroids)
    {
        for (int i = 0; i < inAsteroids.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, inAsteroids[i].transform.position);
            if (distance <= inMaxRange)
            {
                Vector3 endPos = (inAsteroids[i].transform.position - transform.position).normalized * 2.5f;
                endPos += transform.position;
                Debug.DrawLine(transform.position, endPos, Color.green);
            }
        }
    }
}
