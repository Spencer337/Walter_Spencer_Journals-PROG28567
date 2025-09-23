using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TrigExperiments : MonoBehaviour
{
    public List<float> angles = new List<float>();
    public int currentAngleIndex = 0;
    public float radius = 1;
    public Vector3 startPosition = Vector3.zero;
    public float t = 0;
    public float duration = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angles.Add(10);
        angles.Add(30);
        angles.Add(50);
        angles.Add(90);
        angles.Add(120);
        angles.Add(150);
        angles.Add(180);
        angles.Add(200);
        angles.Add(260);
        angles.Add(320);
        // Math f cos sin and tan functions take an angle in radians
    }

    // Update is called once per frame
    void Update()
    {
        float angleInRadians = angles[currentAngleIndex] * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        // Converting the x and y back to the angle
        // Atan2 spits out a value in radians
        float convertedAngle = Mathf.Atan2(y, x);


        Vector3 pointOnCircle = new Vector3(x, y, 0);
        pointOnCircle = pointOnCircle * radius;

        Debug.DrawLine(startPosition, pointOnCircle + startPosition, Color.red);
        t += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || t >= duration)
        {
            currentAngleIndex++;
            t = 0;
        }
        if (currentAngleIndex >= angles.Count)
        {
            currentAngleIndex = 0;
        }
        
    }
}
