using UnityEngine;

public class Guard : MonoBehaviour
{
    public float fovAngle;
    public Transform targetTransform;
    public float coneRadius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float upAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.up.y, transform.up.x);
        float rightConeAngle = upAngle - fovAngle;
        float leftConeAngle = upAngle + fovAngle;

        float rightConeAngleInRadians = rightConeAngle * Mathf.Deg2Rad;
        float leftConeAngleInRadians = leftConeAngle * Mathf.Deg2Rad;

        Vector3 rightConeDirection = new Vector3(Mathf.Cos(rightConeAngleInRadians), Mathf.Sin(rightConeAngleInRadians));
        Vector3 leftConeDirection = new Vector3(Mathf.Cos(leftConeAngleInRadians), Mathf.Sin(leftConeAngleInRadians));

        Vector3 directionToTarget = targetTransform.position - transform.position;
        float angleToTarget = Mathf.Rad2Deg * Mathf.Atan2(directionToTarget.y, directionToTarget.x);

        float differenceInAngles = Mathf.DeltaAngle(angleToTarget, upAngle);

        float distance = Vector2.Distance(transform.position, targetTransform.position);

        Color detectionColour;
        if(fovAngle > Mathf.Abs(differenceInAngles) && distance <= coneRadius)
        {
            detectionColour = Color.green;
        }
        else
        {
            detectionColour = Color.red;
        }

        Debug.DrawLine(transform.position, rightConeDirection * coneRadius + transform.position, detectionColour);
        Debug.DrawLine(transform.position, leftConeDirection * coneRadius + transform.position, detectionColour);
    }
}
