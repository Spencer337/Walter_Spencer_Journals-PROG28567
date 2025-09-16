using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    public float drawingDistance;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }
    public void DrawConstellation()
    {
        //for (int i = 0; i < starTransforms.Count-1; i++)
        //{
        //    Vector3 startPosition = starTransforms[i].position;
        //    Vector3 endPosition = starTransforms[i + 1].position;
        //    Debug.DrawLine(startPosition, endPosition);
        //}
        Vector3 startPosition = starTransforms[0].position;
        Vector3 endPosition = starTransforms[1].position;
        endPosition = (endPosition + startPosition).normalized * drawingDistance;
        drawingDistance += drawingDistance;

    }
}
