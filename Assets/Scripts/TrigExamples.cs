using UnityEngine;

public class TrigExamples : MonoBehaviour
{
    public float currentAngle = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            Vector3 convertedVector = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * 1f;


            Debug.DrawLine(transform.position, convertedVector, Color.red, 3);

            float reconvertedAngle = Mathf.Atan2(convertedVector.y, convertedVector.x);
            Debug.Log("reconverted angle: " + reconvertedAngle * Mathf.Rad2Deg);
        }

    }
}
