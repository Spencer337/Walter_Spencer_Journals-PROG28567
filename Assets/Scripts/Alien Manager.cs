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
    public float rotationSpeed = 2.0f;
    public float rotationOffset = 0;
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
        for (int i = 0; i < aliens.Count; i++)
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
            // Add new point to the list of rotationPoints
            float angle = (360 / rotatingAliens.Count) * i;
            angle += rotationOffset;
            float angleInRadians = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);
            Vector2 newPoint = new Vector2(x, y) * radius;
            rotatingPoints.Add(newPoint);

            // Have the rotating aliens to point towards the player
            Vector3 directionToPlayer = playerTransform.position - rotatingAliens[i].transform.position;
            float currentAngle = Mathf.Rad2Deg * Mathf.Atan2(rotatingAliens[i].transform.up.y, rotatingAliens[i].transform.up.x);
            float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
            float angleRemaining = Mathf.DeltaAngle(currentAngle, targetAngle);
            rotatingAliens[i].transform.Rotate(0f, 0f, angleRemaining);
        }

        rotationOffset += rotationSpeed * Time.deltaTime;
        if (rotationOffset >= 360)
        {
            rotationOffset = 0;
        }

        // Assign those points to the aliens currently rotating around the player
        for (int j = 0; j < rotatingPoints.Count; j++)
        {
            rotatingAliens[j].transform.position = rotatingPoints[j] + (Vector2) playerTransform.position;
        }
        rotatingPoints.Clear();
    }
}
