using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    public float drawingDistance;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 midPosition;
    public float t = 0;
    public int i = 0;
    public float timer;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }
    public void DrawConstellation()
    {
        startPosition = starTransforms[i].position;
        endPosition = starTransforms[i + 1].position;
        midPosition = Vector3.Lerp(startPosition, endPosition, t);
        t += Time.deltaTime / drawingTime;
        Debug.DrawLine(startPosition, midPosition);

        // If the lerp has been completed, meaning the line is fully drawn
        if (t >= 1)
        {
            // Increase I and set t back to zero
            i++;
            t = 0;
        }

        // If the last point has been drawn, set i back to zero to start over
        if (i >= starTransforms.Count - 1)
        {
            i = 0;
        }
    }
}
