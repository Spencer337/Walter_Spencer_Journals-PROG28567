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
    public float speed = 0.5f;
    public float bombSpacing = -0.2f;
    public int numberOfTrailBombs = 3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WarpPlayer(enemyTransform, speed);
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
