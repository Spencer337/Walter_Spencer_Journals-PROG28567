using UnityEngine;

public class AddVectors : MonoBehaviour
{
    public Transform rTransform;
    public Transform bTransform;
    public Vector2 rPlusB;
    public Vector2 rMinusB;
    public Vector2 start = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rVector = rTransform.position;
        Vector2 bVector = bTransform.position;
        rPlusB = rVector + bVector;
        rMinusB = rVector - bVector;    
        if (Input.GetKey(KeyCode.R))
        {
            Debug.DrawLine(start, rVector, Color.red);
        }
        if (Input.GetKey(KeyCode.B))
        {
            Debug.DrawLine (start, bVector, Color.blue);
        }
        //if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.B)) 
        //{
        //    Debug.DrawLine (start, rPlusB, Color.magenta);
        //}

        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.B))
        {
            Debug.DrawLine(start, rMinusB, Color.magenta);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            // Calculate the magnitude of rPlusB
            // magnitude = Mathf.Sqrt ( xValue * xValue + yValue * yValue)
            float mag = Mathf.Abs(Mathf.Sqrt(rPlusB.x * rPlusB.x + rPlusB.y * rPlusB.y));
            Debug.Log(mag);
        }
    }
}
