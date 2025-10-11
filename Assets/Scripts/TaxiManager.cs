using JetBrains.Annotations;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaxiManager : MonoBehaviour
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public GameObject alienObject;
    public GameObject houseObject;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public float t;
    public float timer;
    public bool timerRunning = false;
    public Vector3 minimumPoint;
    public Vector3 maximumPoint;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float score;
    public bool taxiSequenceRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the minimum and maximum points that the alien and house can be spawned at
        minimumPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        maximumPoint = new Vector3(Screen.width, Screen.height, 0);
        maximumPoint = Camera.main.ScreenToWorldPoint(maximumPoint);
        minX = minimumPoint.x + 1;
        minY = minimumPoint.y + 3;
        maxX = maximumPoint.x - 1;
        maxY = maximumPoint.y - 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase t by Time.deltaTime
        t += Time.deltaTime;

        // After the alien has been deactive for three seconds call the SpawnAlienAndHouseMethod
        if (t >= 3 & alienObject.activeSelf == false)
        {
            SpawnAlienAndHouse();
        }

        // Get the distance between the alien and the player
        float distance = Vector2.Distance(playerTransform.position, alienObject.transform.position);

        // If the alien object is active and the player is close to it, start the taxi sequence
        if (distance <= 2 && alienObject.activeSelf == true)
        {
            TaxiSequence(new Vector3(0,1,0));
        }
    }

    public void SpawnAlienAndHouse()
    {
        // Set the alien's transform position to be somewhere randomly on the screen
        Vector3 alienPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        alienObject.transform.position = alienPosition;
        // Set the alien to be active
        alienObject.SetActive(true);

        // Set the house's transform position to be somewhere randomly on the screen
        Vector3 housePosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        houseObject.transform.position = housePosition;
        // Set the house to be active
        houseObject.SetActive(true);
    }

    public void TaxiSequence(Vector3 offset)
    {
        taxiSequenceRunning = true;
        // Set the alien's position to be at the player's plus the offset
        alienObject.transform.position = playerTransform.position + offset;

        // If the timer is not running, calculate the timer's duration based on distance, and set the timer to be running
        if (timerRunning == false)
        {
            timer = Vector2.Distance(playerTransform.position, houseObject.transform.position);
            timerRunning = true;
        }

        // Decrease timer by Time.deltaTime, and update the UI to show the timer's current value
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();

        // If the player has moved close enough to the house, end the taxi sequence and increase score based on the timer
        if (Vector2.Distance(playerTransform.position, houseObject.transform.position) <= 1)
        {
            EndTaxiSequence((int) timer);
        }

        // If the timer runs out, end the taxi sequence and decrease score
        if (timer <= 0)
        {
            EndTaxiSequence(-5);
        }
        // If the distance between enemy and player is less than 1, end the taxi sequence and decrease score
        if (Vector2.Distance(playerTransform.position, enemyTransform.position) <= 1)
        {
            EndTaxiSequence(-5);
        }
    }

    // Set the alien and house to inactive, set t back to 0, set timerRunning back to false
    // Increase the score by the roundScore parameter, and update the score text on the UI
    // Remove the timer text from the UI
    public void EndTaxiSequence(float roundScore)
    {
        alienObject.SetActive(false);
        houseObject.SetActive(false);
        t = 0;
        timerRunning = false;
        score += roundScore;
        scoreText.text = "Score: " + score;
        timerText.text = "";
        taxiSequenceRunning = false;
        alienObject.transform.eulerAngles = Vector3.zero;
    } 
}
