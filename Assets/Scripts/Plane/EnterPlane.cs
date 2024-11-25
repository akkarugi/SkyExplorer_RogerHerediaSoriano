using UnityEngine;

public class Enter_Plane : MonoBehaviour
{
    public GameObject player;           // Referencia al Player original
    public GameObject plane;            // Referencia al avión
    public GameObject playerInPlane;    // Referencia al muñeco que simula conducir el avión
    public GameObject eSprite;          // Sprite de la tecla E
    public Camera planeCamera;          // Cámara del avión
    public Camera playerCamera;         // Cámara del jugador
    public Transform exitPoint;         // Punto de salida del Player al bajar del avión
    public Canvas planeCanvas;          // Canvas del avión
    public MonoBehaviour[] planeScripts; // Lista de scripts del avión a activar/desactivar

    private bool isPlayerInZone = false;
    private bool isPlayerInPlane = false;

    void Start()
    {
        // Asegúrate de que el sprite de "E" esté desactivado al inicio
        eSprite.SetActive(false);

        // Desactiva el muñeco del avión, la cámara y el canvas del avión al inicio
        playerInPlane.SetActive(false);
        planeCamera.gameObject.SetActive(false);
        planeCanvas.gameObject.SetActive(false);

        // Desactiva los scripts del avión al inicio
        TogglePlaneScripts(false);
    }

    void Update()
    {
        if (isPlayerInZone && !isPlayerInPlane)
        {
            // Mostrar el sprite "E" mientras el Player está en el collider
            eSprite.SetActive(true);

            // Si presiona E, se sube al avión
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterPlane();
            }
        }
        else if (isPlayerInPlane)
        {
            // Si está en el avión y presiona R, se baja del avión
            if (Input.GetKeyDown(KeyCode.R))
            {
                ExitPlane();
            }
        }
    }

    private void EnterPlane()
    {
        // Cambiar el estado al avión
        isPlayerInPlane = true;

        // Desactivar al Player original
        player.SetActive(false);

        // Activar el muñeco del avión
        playerInPlane.SetActive(true);

        // Activar los scripts del avión
        TogglePlaneScripts(true);

        // Cambiar la cámara al avión
        planeCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        // Activar el canvas del avión
        planeCanvas.gameObject.SetActive(true);

        // Desactivar el sprite "E"
        eSprite.SetActive(false);
    }

    private void ExitPlane()
    {
        // Cambiar el estado al suelo
        isPlayerInPlane = false;

        // Reactivar al Player original
        player.SetActive(true);
        player.transform.position = exitPoint.position;

        // Desactivar el muñeco del avión
        playerInPlane.SetActive(false);

        // Desactivar los scripts del avión
        TogglePlaneScripts(false);

        // Cambiar la cámara al Player
        planeCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        // Desactivar el canvas del avión
        planeCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el Player entra en la zona
        if (other.gameObject == player)
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectar si el Player sale de la zona
        if (other.gameObject == player)
        {
            isPlayerInZone = false;

            // Asegurarse de ocultar el sprite "E"
            eSprite.SetActive(false);
        }
    }

    private void TogglePlaneScripts(bool state)
    {
        // Activa o desactiva los scripts del avión
        foreach (var script in planeScripts)
        {
            script.enabled = state;
        }
    }
}
