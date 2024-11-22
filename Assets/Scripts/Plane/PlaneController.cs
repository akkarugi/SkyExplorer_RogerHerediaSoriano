using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera planeCamera;
    [SerializeField] private Transform planeExitPoint;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 50f;

    private bool isPlayerInPlane = false;

    private void Start()
    {
        planeCamera.gameObject.SetActive(false); // Desactiva la cámara al inicio
    }

    public void TogglePlaneControl()
    {
        if (!isPlayerInPlane)
        {
            EnterPlane();
        }
        else
        {
            ExitPlane();
        }
    }

    private void EnterPlane()
    {
        isPlayerInPlane = true;
        player.SetActive(false); // Desactiva al jugador
        planeCamera.gameObject.SetActive(true); // Activa la cámara de la avioneta
    }

    private void ExitPlane()
    {
        isPlayerInPlane = false;
        player.SetActive(true); // Reactiva al jugador
        player.transform.position = planeExitPoint.position; // Coloca al jugador fuera de la avioneta
        planeCamera.gameObject.SetActive(false); // Desactiva la cámara de la avioneta
    }

    private void Update()
    {
        if (isPlayerInPlane)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical"); // W/S o flechas arriba/abajo
        float turnInput = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha

        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    }
}
