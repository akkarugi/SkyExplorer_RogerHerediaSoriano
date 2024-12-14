using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, 5, 0);
    }

    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                movingUp = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
                movingUp = true;
        }
    }
}
