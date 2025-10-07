using UnityEngine;

public class DotProduct : MonoBehaviour
{
    public float redAngle = 45;
    public float blueAngle = 150;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float redAngleInRadians = redAngle * Mathf.Deg2Rad;
        float blueAngleInRadians = blueAngle * Mathf.Deg2Rad;
        Vector3 redVector = new Vector3(Mathf.Cos(redAngleInRadians), Mathf.Sin(redAngleInRadians)) * 1f;
        Vector3 blueVector = new Vector3(Mathf.Cos(blueAngleInRadians), Mathf.Sin(blueAngleInRadians)) * 1f;

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float redDotBlue = redVector.x * blueVector.x + redVector.y * blueVector.y;
            Debug.Log("redDotBlue: " + redDotBlue.ToString());
        }
    }
}
