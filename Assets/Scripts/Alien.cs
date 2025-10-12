using UnityEngine;

public class Alien : MonoBehaviour
{
    public TaxiManager taxiManager;
    public Transform playerTransform;
    public float angleInDegrees = 0;
    public float orbitalSpeed = 100;
    public float radius = 1.5f;
    public float angularSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (taxiManager.taxiSequenceRunning == true)
        //{
        //    angleInDegrees += orbitalSpeed * Time.deltaTime;
        //    float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        //    float x = Mathf.Cos(angleInRadians);
        //    float y = Mathf.Sin(angleInRadians);

        //    Vector3 pointOnCircle = new Vector3(x, y, 0);
        //    pointOnCircle = pointOnCircle * radius;

        //    transform.position = pointOnCircle + playerTransform.position;
        //    if (angleInDegrees >= 360)
        //    {
        //        angleInDegrees = 0;
        //    }

        //    Vector3 directionToTarget = playerTransform.position - transform.position;

        //    float dotProduct = Vector3.Dot(transform.right, directionToTarget);
        //    float currentAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.up.y, transform.up.x);
        //    float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(directionToTarget.y, directionToTarget.x);

        //    float angleRemaining = Mathf.DeltaAngle(currentAngle, targetAngle);

        //    transform.Rotate(0f, 0f, angleRemaining);
        //}
    }
}
