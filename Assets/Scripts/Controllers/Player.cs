using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    public Vector2 offset = new Vector2(0,1);
    public float warpSpeed = 0.5f;
    public float bombSpacing = -0.2f;
    public int numberOfTrailBombs = 3;
    public float maxSpeed = 5;
    public float acceleration;
    public float accelerationTime = 2;
    public Vector3 velocity = new Vector3(0, 0, 0);
    public float deceleration;
    public float decelerationTime = 1;
    public float radius = 1;
    public int numberOfPoints = 5;
    public List<Vector3> radarPoints = new List<Vector3>();
    public List<Vector3> powerupPoints = new List<Vector3>();
    public GameObject powerupPrefab;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
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
        PlayerRadar(radius, numberOfPoints);
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPowerups(2, 6);
        }
    }

    public void PlayerMovement()
    {   
        Vector3 direction = Vector3.zero;

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

        // If the player is moving, increase velocity
        if (direction.magnitude > 0)
        {
            velocity += direction.normalized * acceleration * Time.deltaTime;

            // If velocity is greater than the max speed, set it to equal the max speed
            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else
        {
            Vector3 changeInVelocity = velocity.normalized * deceleration * Time.deltaTime;

            if(changeInVelocity.magnitude > velocity.magnitude)
            {
                velocity = Vector3.zero;
            }
            else
            {
                velocity -= changeInVelocity;
            }
        }

        // Move the player by the velocity
        transform.position += velocity * Time.deltaTime;

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

    public void PlayerRadar(float radius, int numberOfPoints)
    {
        Color radarColor = Color.green;
        if (Vector2.Distance(transform.position, enemyTransform.position) <= radius)
        {
            radarColor = Color.red;
        }
        for (int i = 0; i  < numberOfPoints; i++)
        {
            float angle = (360 / numberOfPoints) * i;
            float angleInRadians = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);
            Vector3 newPoint = new Vector3(x, y, 0) * radius;
            radarPoints.Add(newPoint);
        }
        for (int j = 0; j < radarPoints.Count - 1; j++)
        {
            Debug.DrawLine(radarPoints[j] + transform.position, radarPoints[j + 1] + transform.position, radarColor);
        }
        Debug.DrawLine(radarPoints[0] + transform.position, radarPoints[radarPoints.Count - 1] + transform.position, radarColor);
        radarPoints.Clear();
    }

    public void SpawnPowerups(float radius, int numberOfPowerups) 
    {
        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = (360 / numberOfPowerups) * i;
            float angleInRadians = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);
            Vector3 newPoint = new Vector3(x, y, 0) * radius;
            powerupPoints.Add(newPoint);
        }
        for (int j = 0; j < powerupPoints.Count; j++)
        {
            GameObject newPowerup = Instantiate(powerupPrefab, powerupPoints[j] + transform.position, Quaternion.identity);
        }
        powerupPoints.Clear();
    }
}
