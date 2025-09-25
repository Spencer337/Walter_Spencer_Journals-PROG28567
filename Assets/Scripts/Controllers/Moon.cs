using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float radius = 1;
    public float angleInDegrees = 0;
    public float orbitalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angleInDegrees += orbitalSpeed * Time.deltaTime;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        Vector3 pointOnCircle = new Vector3(x, y, 0);
        pointOnCircle = pointOnCircle * radius;

        transform.position = pointOnCircle + planetTransform.position;
        if (angleInDegrees >= 360)
        {
            angleInDegrees = 0;
        }
    }
}
