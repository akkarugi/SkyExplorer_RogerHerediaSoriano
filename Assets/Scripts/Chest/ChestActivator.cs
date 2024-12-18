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

    public bool goToWinScene = false;

    private bool hasOpenedChest = false;
    private bool sceneLoaded = false;

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

                GameManager.Instance?.IncrementChestsOpened();
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

        if (goToWinScene && !sceneLoaded)
        {
            sceneLoaded = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

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
        }
    }
}
