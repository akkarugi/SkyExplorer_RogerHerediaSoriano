using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject interactKeySprite; // El sprite de la tecla E
    public string targetScene = "Platform Level"; // La escena a cargar
    public AudioSource teleportSound; // Sonido de teletransporte

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
            // Hacer que el sprite de la tecla E mire al jugador
            Vector3 direction = (player.position - interactKeySprite.transform.position).normalized;
            direction.y = 0; // Mantener solo la rotación en el plano horizontal
            interactKeySprite.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);

            // Detectar si se presiona la tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                teleportSound.Play();
                SceneManager.LoadScene(targetScene);
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
}
