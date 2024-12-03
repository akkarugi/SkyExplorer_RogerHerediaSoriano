using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject eKeySprite; // Sprite de la tecla "E"
    public GameObject dialogueCanvas; // Canvas del di�logo
    public Transform player; // Transform del jugador (para orientar el sprite hacia �l)

    private bool isPlayerInRange = false; // Si el jugador est� dentro del rango

    private void Start()
    {
        // Aseg�rate de que los elementos est�n inicialmente desactivados
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
                // Si el jugador est� dentro y presiona "E", activa el di�logo
                eKeySprite.SetActive(false); // Desactiva el sprite de la tecla "E"
                dialogueCanvas.SetActive(true); // Activa el Canvas del di�logo
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
            // Calcula la direcci�n hacia el jugador
            Vector3 direction = (player.position - sprite.transform.position).normalized;

            // Calcula la rotaci�n para mirar hacia el jugador
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Ajusta la rotaci�n si es necesario (por ejemplo, invertir el eje)
            lookRotation *= Quaternion.Euler(0, 180, 0);

            // Aplica la rotaci�n al sprite
            sprite.transform.rotation = lookRotation;
        }
    }
}
