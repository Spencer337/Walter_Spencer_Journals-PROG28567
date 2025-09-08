using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public Vector2 mousePos;
    public Vector2 newPoint;
    public Vector2 oldPoint;
    public float t;
    public float magnitude;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            
            t += Time.deltaTime;
            if (t >0.1)
            {
                newPoint = mousePos;
                Debug.DrawLine(oldPoint, newPoint, Color.white, 100);
                magnitude += Mathf.Abs(Mathf.Sqrt(newPoint.x * newPoint.x + newPoint.y * newPoint.y));
                oldPoint = newPoint;
                t = 0;
            }
        }
        else
        {
            oldPoint = mousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(magnitude);
            magnitude = 0;
        }
    }
}
