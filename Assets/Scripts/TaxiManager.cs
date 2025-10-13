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
    public Transform secondEnemyTransform;
    public AlienManager alienManager;
    public GameObject houseObject;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public float t;
    public float timer;
    public bool timerRunning = false;
    public float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Increase t by Time.deltaTime
        t += Time.deltaTime;

        // After the alien has been deactive for three seconds call the SpawnAlienAndHouseMethod
        if (t >= 3 && houseObject.activeSelf == false)
        {
            SpawnAlienAndHouse();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnAlienAndHouse();
        }

        // If the alien object is active and the player is close to it, start the taxi sequence
        if (alienManager.rotatingAliens.Count > 0)
        {
            //Debug.Log("Touching");
            TaxiSequence();
        }
    }

    public void SpawnAlienAndHouse()
    {
        alienManager.spawnAliens();
        // Set the house's transform position to be somewhere randomly on the screen
        Vector2 housePosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
        houseObject.transform.position = housePosition;
        // Set the house to be active
        houseObject.SetActive(true);
    }

    public void TaxiSequence()
    {
        //If the timer is not running, calculate the timer's duration based on distance multiplied by the number of aliens
        //Set the timer to be running
        if (timerRunning == false)
        {
            timer = Vector2.Distance(playerTransform.position, houseObject.transform.position) * alienManager.numberOfAliens;
            timerRunning = true;
        }

        // Decrease timer by Time.deltaTime, and update the UI to show the timer's current value
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();

        // If the player has moved close enough to the house and has all the aliens,
        // end the taxi sequence and increase score based on the timer
        if (Vector2.Distance(playerTransform.position, houseObject.transform.position) <= 1 && alienManager.rotatingAliens.Count == alienManager.numberOfAliens)
        {
            EndTaxiSequence((int)timer);
        }

        // If the timer runs out, end the taxi sequence and decrease score
        if (timer <= 0)
        {
            EndTaxiSequence(-5);
        }
        // If the distance between enemy and player is less than 1, end the taxi sequence and decrease score
        if (Vector2.Distance(playerTransform.position, enemyTransform.position) <= 1 || Vector2.Distance(playerTransform.position, secondEnemyTransform.position) <= 1)
        {
            EndTaxiSequence(-5);
        }
    }

    // Set the house to inactive, set t back to 0, set timerRunning back to false
    // Increase the score by the roundScore parameter, and update the score text on the UI
    // Remove the timer text from the UI
    // Call the method in alienManager to remove all aliens from the scene
    public void EndTaxiSequence(float roundScore)
    {
        houseObject.SetActive(false);
        t = 0;
        timerRunning = false;
        score += roundScore;
        scoreText.text = "Score: " + score;
        timerText.text = "";
        alienManager.ResetAliens();
    } 
}
