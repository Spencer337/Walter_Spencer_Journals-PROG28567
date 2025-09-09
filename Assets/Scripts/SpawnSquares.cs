using UnityEngine;

public class SpawnSquares : MonoBehaviour
{
    public float radius;
    public Vector2 start;
    public Vector2 end;
    public Vector2 mousePos;
    public Color semiTransparent = new Color (1f, 1f, 1f, 0.5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radius = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        radius += Input.mouseScrollDelta.y * 0.1f;
        DrawSquare(semiTransparent, 0);

        if (Input.GetMouseButton(0))
        {
            DrawSquare(Color.white, 100);
        }
        MathFunctions.DrawSquare(mousePos, radius - 0.1f, Color.red, 2);
    }

    public void DrawSquare(Color c, int d)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        start.x = mousePos.x - radius;
        start.y = mousePos.y + radius;
        end.x = mousePos.x + radius;
        end.y = mousePos.y + radius;
        Debug.DrawLine(start, end, c, d);
        start = end;
        end.y = end.y - radius - radius;
        Debug.DrawLine(start, end, c, d);
        start = end;
        end.x = end.x - radius - radius;
        Debug.DrawLine(start, end, c, d);
        start = end;
        end.y = end.y + radius + radius;
        Debug.DrawLine(start, end, c, d);
    }
}
