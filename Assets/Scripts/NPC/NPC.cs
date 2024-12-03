using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject eKeySprite; // Sprite de la tecla "E"
    public GameObject dialogueCanvas; // Canvas del diálogo
    public Transform player; // Transform del jugador (para orientar el sprite hacia él)

    private bool isPlayerInRange = false; // Si el jugador está dentro del rango

    private void Start()
    {
        // Asegúrate de que los elementos estén inicialmente desactivados
        eKeySprite.SetActive(false);
        dialogueCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            LookAtPlayer(eKeySprite); // Hace que el sprite de la tecla "E" mire hacia el jugador

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Si el jugador está dentro y presiona "E", activa el diálogo
                eKeySprite.SetActive(false); // Desactiva el sprite de la tecla "E"
                dialogueCanvas.SetActive(true); // Activa el Canvas del diálogo
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador entra en el rango, muestra el sprite de la tecla "E"
            isPlayerInRange = true;
            eKeySprite.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador sale del rango, desactiva el sprite y el Canvas
            isPlayerInRange = false;
            eKeySprite.SetActive(false);
            dialogueCanvas.SetActive(false);
        }
    }

    private void LookAtPlayer(GameObject sprite)
    {
        if (sprite != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - sprite.transform.position).normalized;

            // Calcula la rotación para mirar hacia el jugador
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Ajusta la rotación si es necesario (por ejemplo, invertir el eje)
            lookRotation *= Quaternion.Euler(0, 180, 0);

            // Aplica la rotación al sprite
            sprite.transform.rotation = lookRotation;
        }
    }
}
