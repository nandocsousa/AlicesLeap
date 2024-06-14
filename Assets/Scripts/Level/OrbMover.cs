using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;

    private Vector2 targetPosition;
    private bool movingToB = true;

    void Start()
    {
        targetPosition = pointB.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (movingToB)
            {
                targetPosition = pointA.position;
            }
            else
            {
                targetPosition = pointB.position;
            }
            movingToB = !movingToB;
        }
    }
}
