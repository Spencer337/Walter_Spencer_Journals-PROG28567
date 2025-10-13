using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float frequency = 5f;
    public float amplitude = 5f;
    public Vector3 pos;
    public float moveSpeed = 10;
    public float t;

    private void Start()
    {
        pos = transform.position;
    }
    private void Update()
    {
        moveAlongWave();
    }

    public void moveAlongWave()
    {
        // Moving horizontally
        pos.x += Time.deltaTime * moveSpeed;

        // Increment t based on time
        t += Time.deltaTime;

        // Moving vertically
        transform.position = pos + transform.up * Mathf.Sin(t * frequency) * amplitude;

        // If at the edge of the screen, invert move speed
        Vector2 cameraPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (cameraPosition.x < 0)
        {
            moveSpeed = moveSpeed * -1;
            pos.x = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        }
        if (cameraPosition.x > Screen.width)
        {
            moveSpeed = moveSpeed * -1;
            Vector3 maximumPoint = new Vector3(Screen.width, Screen.height, 0);
            pos.x = Camera.main.ScreenToWorldPoint(maximumPoint).x;
        }
    }

}
