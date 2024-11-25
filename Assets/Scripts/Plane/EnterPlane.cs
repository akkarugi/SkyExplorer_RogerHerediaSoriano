using UnityEngine;

public class Enter_Plane : MonoBehaviour
{
    public GameObject player;
    public GameObject plane;
    public GameObject playerInPlane;
    public GameObject eSprite;
    public Camera planeCamera;
    public Camera playerCamera;
    public Transform exitPoint;
    public Canvas planeCanvas;
    public MonoBehaviour[] planeScripts;

    private bool isPlayerInZone = false;
    private bool isPlayerInPlane = false;

    void Start()
    {
        eSprite.SetActive(false);
        playerInPlane.SetActive(false);
        planeCamera.gameObject.SetActive(false);
        planeCanvas.gameObject.SetActive(false);
        TogglePlaneScripts(false);
    }

    void Update()
    {
        if (isPlayerInZone && !isPlayerInPlane)
        {
            LookAtPlayer(eSprite);
            eSprite.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterPlane();
            }
        }
        else if (isPlayerInPlane)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ExitPlane();
            }
        }
    }

    private void LookAtPlayer(GameObject sprite)
    {
        Vector3 direction = (player.transform.position - sprite.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 180, 0);
        sprite.transform.rotation = lookRotation;
    }

    private void EnterPlane()
    {
        isPlayerInPlane = true;
        player.SetActive(false);
        playerInPlane.SetActive(true);
        TogglePlaneScripts(true);
        planeCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
        planeCanvas.gameObject.SetActive(true);
        eSprite.SetActive(false);
    }

    private void ExitPlane()
    {
        isPlayerInPlane = false;
        player.SetActive(true);
        player.transform.position = exitPoint.position;
        playerInPlane.SetActive(false);
        TogglePlaneScripts(false);
        planeCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        planeCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInZone = false;
            eSprite.SetActive(false);
        }
    }

    private void TogglePlaneScripts(bool state)
    {
        foreach (var script in planeScripts)
        {
            script.enabled = state;
        }
    }
}
