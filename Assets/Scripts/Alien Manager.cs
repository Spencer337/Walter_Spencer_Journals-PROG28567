using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public GameObject alienPrefab;
    public Transform playerTransform;

    public List <GameObject> aliens;
    public List<GameObject> rotatingAliens;
    public List<Vector2> rotatingPoints;

    public float radius = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aliens = new List <GameObject>();
        rotatingAliens = new List <GameObject>();
        rotatingPoints = new List <Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        touchingPlayer();
        rotateAroundPlayer();
        
    }

    public void spawnAliens()
    {
        for (int i = 0; i < (int)Random.Range(1, 6); i++)
        {
            Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0,Screen.width) , Random.Range(0,Screen.height) ));
            GameObject newAlien = Instantiate( alienPrefab, spawnPosition, Quaternion.identity);
            aliens.Add(newAlien);
        }
    }

    public void touchingPlayer ()
    {
        for (int i = 0;i < aliens.Count; i++)
        {
            float distance = Vector2.Distance(playerTransform.position, aliens[i].transform.position);
            if (distance <= 2)
            {
                    rotatingAliens.Add(aliens[i]);
                    aliens.Remove(aliens[i]);
            }
        }      
    }

    public void rotateAroundPlayer()
    { 
        // Gain all the points that the aliens will be at
        for (int i = 0; i < rotatingAliens.Count; i++)
        {
            float angle = (360 / rotatingAliens.Count) * i;
            float angleInRadians = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);
            Vector2 newPoint = new Vector2(x, y) * radius;
            rotatingPoints.Add(newPoint);
        }

        // Assign those points to the aliens currently rotating around the player
        for (int j = 0; j < rotatingPoints.Count; j++)
        {
            rotatingAliens[j].transform.position = rotatingPoints[j] + (Vector2) playerTransform.position;
        }
        rotatingPoints.Clear();
    }
}
