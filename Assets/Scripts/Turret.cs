using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Color = UnityEngine.Color;

public class Turret : MonoBehaviour
{
    public float angularSpeed = 1.0f;
    public Transform targetTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.up + transform.position);
        Debug.DrawLine(transform.position, targetTransform.position, Color.blue);

        Vector3 directionToTarget = targetTransform.position - transform.position;


        float dotProduct = Vector3.Dot(transform.right, directionToTarget);
        float currentAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.up.y, transform.up.x);
        float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(directionToTarget.y, directionToTarget.x);

        float angleRemaining = Mathf.DeltaAngle(currentAngle, targetAngle);
        float changeInAngle;

        //If dot product is positive
        //The object is on the right hand side and we should rotate to the right
        if (dotProduct > 0)
        {
            //Negative will rotate to the right
            changeInAngle = -angularSpeed * Time.deltaTime;

            //If the change that we're about to do would overshoot
            if (changeInAngle < angleRemaining)
            {
                //Snap to the target
                transform.Rotate(0f, 0f, angleRemaining);
            }
            else //If it wouldn't overshoot
            {
                //Rotate as normal
                transform.Rotate(0f, 0f, changeInAngle);
            }


        }
        else
        {
            changeInAngle = angularSpeed * Time.deltaTime;
            //Move to the left
            if (changeInAngle > angleRemaining)
            {
                transform.Rotate(0f, 0f, angleRemaining);
            }
            else
            {
                transform.Rotate(0f, 0f, changeInAngle);
            }
        }

    }
}
