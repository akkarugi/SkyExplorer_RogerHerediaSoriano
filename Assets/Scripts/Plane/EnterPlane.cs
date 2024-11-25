using UnityEngine;

public class Enter_Plane : MonoBehaviour
{
    public GameObject player;           // Referencia al Player original
    public GameObject plane;            // Referencia al avi�n
    public GameObject playerInPlane;    // Referencia al mu�eco que simula conducir el avi�n
    public GameObject eSprite;          // Sprite de la tecla E
    public Camera planeCamera;          // C�mara del avi�n
    public Camera playerCamera;         // C�mara del jugador
    public Transform exitPoint;         // Punto de salida del Player al bajar del avi�n
    public Canvas planeCanvas;          // Canvas del avi�n
    public MonoBehaviour[] planeScripts; // Lista de scripts del avi�n a activar/desactivar

    private bool isPlayerInZone = false;
    private bool isPlayerInPlane = false;

    void Start()
    {
        // Aseg�rate de que el sprite de "E" est� desactivado al inicio
        eSprite.SetActive(false);

        // Desactiva el mu�eco del avi�n, la c�mara y el canvas del avi�n al inicio
        playerInPlane.SetActive(false);
        planeCamera.gameObject.SetActive(false);
        planeCanvas.gameObject.SetActive(false);

        // Desactiva los scripts del avi�n al inicio
        TogglePlaneScripts(false);
    }

    void Update()
    {
        if (isPlayerInZone && !isPlayerInPlane)
        {
            // Mostrar el sprite "E" mientras el Player est� en el collider
            eSprite.SetActive(true);

            // Si presiona E, se sube al avi�n
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterPlane();
            }
        }
        else if (isPlayerInPlane)
        {
            // Si est� en el avi�n y presiona R, se baja del avi�n
            if (Input.GetKeyDown(KeyCode.R))
            {
                ExitPlane();
            }
        }
    }

    private void EnterPlane()
    {
        // Cambiar el estado al avi�n
        isPlayerInPlane = true;

        // Desactivar al Player original
        player.SetActive(false);

        // Activar el mu�eco del avi�n
        playerInPlane.SetActive(true);

        // Activar los scripts del avi�n
        TogglePlaneScripts(true);

        // Cambiar la c�mara al avi�n
        planeCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        // Activar el canvas del avi�n
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

        // Desactivar el mu�eco del avi�n
        playerInPlane.SetActive(false);

        // Desactivar los scripts del avi�n
        TogglePlaneScripts(false);

        // Cambiar la c�mara al Player
        planeCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        // Desactivar el canvas del avi�n
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
        // Activa o desactiva los scripts del avi�n
        foreach (var script in planeScripts)
        {
            script.enabled = state;
        }
    }
}
