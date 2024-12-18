using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject interactKeySprite;
    public Transform teleportTarget;
    public AudioSource teleportSound;
    public AudioSource soundToStop;
    public GameObject objectToDeactivate;
    public Camera targetCamera;
    public Material newSkybox;

    private Transform player;
    private bool isPlayerInRange;

    private void Start()
    {
        if (interactKeySprite != null)
        {
            interactKeySprite.SetActive(false);
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
            objectToDeactivate?.SetActive(false);
            ChangeCameraSkybox();
        }

        if (isPlayerInRange && interactKeySprite != null)
        {
            RotateInteractSpriteTowardsPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactKeySprite?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactKeySprite?.SetActive(false);
        }
    }

    private void TeleportPlayer()
    {
        teleportSound?.Play();
        if (soundToStop != null && soundToStop.isPlaying)
        {
            soundToStop.Stop();
        }

        if (teleportTarget != null && player != null)
        {
            player.position = teleportTarget.position;
        }
    }

    private void ChangeCameraSkybox()
    {
        if (targetCamera != null && newSkybox != null)
        {
            Skybox cameraSkybox = targetCamera.GetComponent<Skybox>() ?? targetCamera.gameObject.AddComponent<Skybox>();
            cameraSkybox.material = newSkybox;
        }
    }

    private void RotateInteractSpriteTowardsPlayer()
    {
        Vector3 direction = (player.position - interactKeySprite.transform.position).normalized;
        direction.y = 0;
        interactKeySprite.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
    }
}
