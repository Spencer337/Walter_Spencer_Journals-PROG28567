using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    public Vector3 destination;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // Choose a new point as a vector2, assigning it's x and y values to random numbers
        destination = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

        // Normalize the point
        destination = destination.normalized;

        // Multiply point by maxFloatDistance
        destination = destination * maxFloatDistance;
        destination = destination + transform.position;
        //Debug.Log(Vector2.Distance(transform.position, destination));
        //Debug.Log(destination);
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        //Debug.DrawLine(transform.position, destination);
        //Debug.Log(destination.ToString());

        // Move asteroid toward that point based on moveSpeed
        direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed;
        float distance = Vector2.Distance(transform.position, destination);
        //Debug.Log(distance);
        if (distance <= arrivalDistance)
        {
            //Debug.Log("In distance");
            // Choose a new point as a vector2, assigning it's x and y values to random numbers
            destination = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

            // Normalize the point
            destination = destination.normalized;
            //destination = destination.normalized;

            // Multiply point by maxFloatDistance
            destination = destination * maxFloatDistance;
            destination = destination + transform.position;
            //Debug.Log(Vector2.Distance(transform.position, destination));
        }
    }
}
