using JetBrains.Annotations;
using UnityEngine;

public class TaxiManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject alienObject;
    public GameObject houseObject;
    public float t;
    public Vector3 minimumPoint;
    public Vector3 maximumPoint;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the minimum and maximum points that the alien and house can be spawned at
        minimumPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        maximumPoint = new Vector3(Screen.width, Screen.height, 0);
        maximumPoint = Camera.main.ScreenToWorldPoint(maximumPoint);
        minX = minimumPoint.x;
        minY = minimumPoint.y;
        maxX = maximumPoint.x;
        maxY = maximumPoint.y;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        // After the alien has been deactive for three seconds call the SpawnAlienAndHouseMethod
        if (t >= 3 & alienObject.activeSelf == false)
        {
            SpawnAlienAndHouse();
        }

        // Get the distance between the alien and the player
        float distance = Vector2.Distance(playerTransform.position, alienObject.transform.position);

        // If the alien object is active and the player is close to it, start the taxi sequence
        if (distance <= 1.5 && alienObject.activeSelf == true)
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
        // Set the alien's position to be at the player's plus the offset
        alienObject.transform.position = playerTransform.position + offset;

        // If the player has moved close enough to the house, make the alien and house inactive, and reset the timer
        if (Vector2.Distance(playerTransform.position, houseObject.transform.position) <= 1)
        {
            alienObject.SetActive(false);
            houseObject.SetActive(false);
            t = 0;
        }
    }
}
