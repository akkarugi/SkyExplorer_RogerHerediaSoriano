using UnityEngine;

public class PlaneMovementMenu : MonoBehaviour
{
    public float radius = 5f;
    public float speed = 1f;
    public bool enableVerticalOscillation = true;
    public float verticalAmplitude = 1f;
    public Transform propeller;
    public float propellerRotationSpeed = 500f;

    private float angle = 0f;

    private void Update()
    {
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        float y = enableVerticalOscillation ? Mathf.Sin(angle) * verticalAmplitude : 0f;

        transform.position = new Vector3(x, y, z);
        transform.LookAt(new Vector3(0, y, 0));

        if (propeller != null)
        {
            propeller.Rotate(Vector3.right * propellerRotationSpeed * Time.deltaTime);
        }
    }
}
