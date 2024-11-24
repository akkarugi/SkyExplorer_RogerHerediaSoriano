using UnityEngine;

public class Enter_Plane : MonoBehaviour
{
    [SerializeField] private GameObject eSprite;
    [SerializeField] private PlaneMovement planeMovement;
    [SerializeField] private Camera planeCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform planeExitPoint;

    private bool isNearPlane = false;
    private bool isPlayerInPlane = false;

    private void Start()
    {
        eSprite.SetActive(false);
        planeMovement.enabled = false;
        planeCamera.gameObject.SetActive(false);
        player.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isPlayerInPlane)
            {
                Debug.Log("Intentando salir del avión...");
                ExitPlane();
            }
            else if (isNearPlane)
            {
                Debug.Log("Intentando entrar al avión...");
                EnterPlane();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Forzando salida del avión...");
            ExitPlane();
        }
    }

    private void EnterPlane()
    {
        Debug.Log("Entrando al avión...");
        isPlayerInPlane = true;

        player.SetActive(false);
        eSprite.SetActive(false);

        planeMovement.enabled = true;
        planeCamera.gameObject.SetActive(true);
    }

    private void ExitPlane()
    {
        Debug.Log("Saliendo del avión...");
        isPlayerInPlane = false;

        player.SetActive(true);
        player.transform.position = planeExitPoint.position;

        planeMovement.enabled = false;
        planeCamera.gameObject.SetActive(false);

        if (isNearPlane)
        {
            eSprite.SetActive(true);
        }

        Debug.Log("Jugador activado y colocado en el punto de salida.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            isNearPlane = true;

            if (!isPlayerInPlane)
            {
                eSprite.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            isNearPlane = false;
            eSprite.SetActive(false);
        }
    }
}
