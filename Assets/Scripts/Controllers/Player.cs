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
        //Vector2 n = new Vector2(directionToMove.x / Mathf.Abs(directionToMove.x) , directionToMove.y / Mathf.Abs(directionToMove.y));
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
}
