using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello World");
        //Create a start vector at 0,0 create an end vector with a value 0,1
        Vector2 start = new Vector2(0, 0);
        Vector2 end = new Vector2(0, 1);
        
        Debug.DrawLine(start, end, Color.yellow);

        end = new Vector2(3, -2);
        Debug.DrawLine (start, end, Color.grey);
    }
}
