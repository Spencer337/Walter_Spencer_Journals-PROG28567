using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = target.position - transform.position;

        Vector3 changeInPosition = directionToTarget.normalized * speed * Time.deltaTime;

        if (directionToTarget.magnitude > changeInPosition.magnitude)
        {
            transform.position += changeInPosition;
        }
        else
        {
            transform.position = target.position;
        }
        
    }
}
