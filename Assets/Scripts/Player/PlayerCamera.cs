using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 300f;
    [SerializeField] private float minY = -60f;
    [SerializeField] private float maxY = 60f;
    [SerializeField] private Transform cameraTransform;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Cursor.lockState.Equals(CursorLockMode.Locked);
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotationX, 0f);
    }
}
