using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestActivator : MonoBehaviour
{
    public GameObject openButtonSprite;
    public Transform player;
    public Collider canOpenCollider;
    public ChestController chestController;
    public GameObject canvas;
    public ChestCounterUI chestCounterUI;

    [Header("Opcional: Cambiar de escena")]
    public bool goToWinScene = false;

    private bool hasOpenedChest = false;

    private void Start()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (hasOpenedChest) return;

        if (IsPlayerInCanOpenZone())
        {
            ShowOpenButton();
            LookAtPlayer(openButtonSprite);

            if (Input.GetKeyDown(KeyCode.E))
            {
                chestController.OpenChest();
                Destroy(openButtonSprite);
                hasOpenedChest = true;
                chestCounterUI?.IncrementChestCount();

                StartCoroutine(HandlePostChestOpen());
            }
        }
        else
        {
            HideOpenButton();
        }
    }

    private bool IsPlayerInCanOpenZone() => canOpenCollider.bounds.Contains(player.position);

    private void ShowOpenButton()
    {
        if (openButtonSprite != null)
        {
            openButtonSprite.SetActive(true);
        }
    }

    private void HideOpenButton()
    {
        if (openButtonSprite != null)
        {
            openButtonSprite.SetActive(false);
        }
    }

    private void LookAtPlayer(GameObject sprite)
    {
        if (sprite != null)
        {
            Vector3 direction = (player.position - sprite.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 180, 0);
            sprite.transform.rotation = lookRotation;
        }
    }

    private System.Collections.IEnumerator HandlePostChestOpen()
    {
        yield return new WaitForSeconds(2f);

        if (canvas != null)
        {
            canvas.SetActive(true);
        }

        if (goToWinScene)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Verificar el número de mapas y cargar la escena adecuada
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.mapsCollected > 50)
                {
                    SceneManager.LoadScene("Win");
                }
                else
                {
                    SceneManager.LoadScene("NoWin");
                }
            }
            else
            {
                Debug.LogError("GameManager.Instance es nulo. Asegúrate de que el GameManager esté en la escena.");
            }
        }
    }
}
