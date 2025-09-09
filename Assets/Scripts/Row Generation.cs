using UnityEngine;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public float radius;
    public Vector2 start;
    public Vector2 end;
    public Vector2 drawPoint;
    public Vector2 originPoint;
    public int numOfSquares;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPoint.x = 0;
        originPoint.y = Screen.height / 2;
        drawPoint = Camera.main.ScreenToWorldPoint(originPoint);
        radius = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numOfSquares; i++)
        {
            drawPoint.x += 1;
            start.x = drawPoint.x - radius;
            start.y = drawPoint.y + radius;
            end.x = drawPoint.x + radius;
            end.y = drawPoint.y + radius;
            Debug.DrawLine(start, end);
            start = end;
            end.y = end.y - radius - radius;
            Debug.DrawLine(start, end);
            start = end;
            end.x = end.x - radius - radius;
            Debug.DrawLine(start, end);
            start = end;
            end.y = end.y + radius + radius;
            Debug.DrawLine(start, end);
        }
        drawPoint = Camera.main.ScreenToWorldPoint(originPoint);
    }

    public void GenerateSquares()
    {
        string input = inputField.text;
        if (int.Parse(input) > 0)
        {
            numOfSquares = int.Parse(input);
        }
        else
        {
            Debug.Log("Number is invalid, please input a number greater than 0");
        }
    }
}