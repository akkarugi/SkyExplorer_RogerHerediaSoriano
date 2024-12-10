using System.Collections;
using UnityEngine;
using TMPro;

public class DeviChallenge : MonoBehaviour
{
    public GameObject ringsContainer; // El empty con los aros.
    public TextMeshProUGUI timerText; // El TMP para mostrar el tiempo.
    public float challengeDuration = 120f; // Duración de la prueba en segundos.
    public GameObject objectToDestroy; // El objeto que será destruido al completar el desafío.

    private float remainingTime;
    private bool challengeActive = false;
    private int collectiblesRemaining;
    private bool playerOnPlane = false;

    private void Update()
    {
        if (playerOnPlane && Input.GetKeyDown(KeyCode.E)) // Activa el desafío al presionar "E".
        {
            StartChallenge();
        }

        if (challengeActive)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                EndChallenge(false);
            }

            UpdateTimerUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlane = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlane = false;
        }
    }

    public void StartChallenge()
    {
        ringsContainer.SetActive(true); // Activa el Empty con los aros y el temporizador.
        timerText.gameObject.SetActive(true); // Activa el texto del temporizador.
        remainingTime = challengeDuration;
        collectiblesRemaining = ringsContainer.transform.childCount;
        challengeActive = true;
        playerOnPlane = false; // Evita que el desafío se active más de una vez.
    }

    public void CollectItem(GameObject collectible)
    {
        collectiblesRemaining--;
        Destroy(collectible);

        if (collectiblesRemaining <= 0 && challengeActive)
        {
            EndChallenge(true);
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    private void EndChallenge(bool success)
    {
        challengeActive = false;
        timerText.gameObject.SetActive(false); // Desactiva el temporizador.
        ringsContainer.SetActive(false); // Desactiva los aros.

        if (success && objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Devi") && challengeActive)
        {
            CollectItem(other.gameObject);
        }
    }
}
