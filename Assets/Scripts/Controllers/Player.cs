using System.Collections;
using System.Collections.Generic;
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
        // Formula for normalizing a vector: Vector2 n = directionToMove / directionToMove.magnitude;

        //Debug.Log(n);
        //Debug.Log(directionToMove.normalized);

        if (Input.GetKeyDown(KeyCode.W))
        {
            WarpPlayer(speed);
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
    }

    public void SpawnBombAtOffset(Vector2 inOffset)
    {
        Vector2 spawnPosition = (Vector2)transform.position + offset;
        GameObject newBomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }

    public void WarpPlayer (float speed)
    {
        Vector2 targetPosition = enemyTransform.position;
        Vector2 startPosition = transform.position;
        Vector2 directionToMove = targetPosition - startPosition;
        //transform.position += (Vector3)directionToMove.normalized * speed;
        transform.position = Vector2.Lerp(startPosition, targetPosition, speed);
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
        int direction = Random.Range(0, 4);
        GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        newBomb.transform.Rotate(0, 0, 45);
        // Spawns a bomb to the top left of the player
        if (direction == 0)
        {
            Debug.Log(direction);
            newBomb.transform.position += newBomb.transform.up * inDistance;
            float distance = Vector2.Distance(transform.position, newBomb.transform.position);
            Debug.Log(distance);
        }
        // Spawns a bomb to the top right of the player
        else if (direction == 1)
        {
            Debug.Log(direction);
            newBomb.transform.position += newBomb.transform.right * inDistance;
            float distance = Vector2.Distance(transform.position, newBomb.transform.position);
            Debug.Log(distance);
        }
        // Spawns a bomb to the bottom right of the player
        else if (direction == 2)
        {
            Debug.Log(direction);
            newBomb.transform.position += newBomb.transform.up * -inDistance;
            float distance = Vector2.Distance(transform.position, newBomb.transform.position);
            Debug.Log(distance);
        }
        // Spawns a bomb to the bottom left of the player
        else if (direction == 3)
        {
            Debug.Log(direction);
            newBomb.transform.position += newBomb.transform.right * -inDistance;
            float distance = Vector2.Distance(transform.position, newBomb.transform.position);
            Debug.Log(distance);
        }

    }
}
