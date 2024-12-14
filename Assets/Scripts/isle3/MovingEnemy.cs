using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(10f, 0, 0); 
    }

    void Update()
    {
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                movingForward = false; // Cambiar dirección al llegar al objetivo
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
                movingForward = true; // Cambiar dirección al regresar al inicio
        }
    }
}
