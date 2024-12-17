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
        interactKeySprite.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = (player.position - interactKeySprite.transform.position).normalized;
            direction.y = 0;
            interactKeySprite.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);

            if (Input.GetKeyDown(KeyCode.E))
            {
                TeleportPlayer();
                DeactivateObject();
                ChangeCameraSkybox();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactKeySprite.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactKeySprite.SetActive(false);
        }
    }

    private void TeleportPlayer()
    {
        if (teleportSound != null)
        {
            teleportSound.Play();
        }

        if (soundToStop != null && soundToStop.isPlaying)
        {
            soundToStop.Stop(); 
        }

        if (teleportTarget != null && player != null)
        {
            player.position = teleportTarget.position;
        }
    }

    private void DeactivateObject()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
    }

    private void ChangeCameraSkybox()
    {
        if (targetCamera != null && newSkybox != null)
        {
            Skybox cameraSkybox = targetCamera.GetComponent<Skybox>();
            if (cameraSkybox == null)
            {
                cameraSkybox = targetCamera.gameObject.AddComponent<Skybox>();
            }
            cameraSkybox.material = newSkybox;
        }
    }
}
