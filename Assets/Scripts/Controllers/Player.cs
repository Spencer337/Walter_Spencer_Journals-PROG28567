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
    public float accelerationTime = 3;
    public float bombSpacing = -0.2f;
    public int numberOfTrailBombs = 3;
    public Vector3 velocity = new Vector3(0, 0, 0);

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
        //Way 1
        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);
        //transform.Translate(0, Input.GetAxis("Vertical") * Time.deltaTime, 0);

        // Way 2
        //velocity.x = Input.GetAxisRaw("Horizontal");
        //velocity.y = Input.GetAxisRaw("Vertical");
        //transform.position += velocity * Time.deltaTime;

        // Way 3
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    velocity += Vector3.right * Time.deltaTime;
        //    transform.position += velocity * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    velocity += Vector3.left * Time.deltaTime;
        //    transform.position += velocity * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    velocity += Vector3.up * Time.deltaTime;
        //    transform.position += velocity * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    velocity += Vector3.down * Time.deltaTime;
        //    transform.position += velocity * Time.deltaTime;
        //}
        //if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    velocity.x = 0;
        //}
        //if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    velocity.y = 0;
        //}
        //if (velocity.x > maxSpeed)
        //{
        //    velocity.x = maxSpeed;
        //}
        //if (velocity.y > maxSpeed)
        //{
        //    velocity.y = maxSpeed;
        //}
        //if (velocity.x < -maxSpeed)
        //{
        //    velocity.x = -maxSpeed;
        //}
        //if (velocity.y < -maxSpeed)
        //{
        //    velocity.y = -maxSpeed;
        //}

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
        // When up key is pressed, increase the direction to the up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.up;
        }
        // When down key is pressed, increase the direction to the down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.right;
        }
        // Normalize direction vector
        direction = Vector3.Normalize(direction);
        velocity += (Vector3)direction * acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        //Somewhere in your code, you will subtract the deceleration
        //When you stop pressing input
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
