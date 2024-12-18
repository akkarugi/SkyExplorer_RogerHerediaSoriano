using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToScene : MonoBehaviour
{
    public GameObject interactKeySprite;
    public string sceneToLoad;
    public AudioSource teleportSound;
    public GameObject objectToDeactivate;

    private bool isPlayerInRange;

    private void Start()
    {
        if (interactKeySprite != null)
        {
            interactKeySprite.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            UsePortal();
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

    private void UsePortal()
    {
        teleportSound?.Play();
        objectToDeactivate?.SetActive(false);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
