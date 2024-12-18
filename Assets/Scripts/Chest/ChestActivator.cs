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

        if (canOpenCollider.bounds.Contains(player.position))
        {
            openButtonSprite?.SetActive(true);

            Vector3 direction = (player.position - openButtonSprite.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
            openButtonSprite.transform.rotation = lookRotation;

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenChestOnce();
            }
        }
        else
        {
            openButtonSprite?.SetActive(false);
        }
    }

    private void OpenChestOnce()
    {
        if (hasOpenedChest) return;

        hasOpenedChest = true;
        chestController.OpenChest();
        Destroy(openButtonSprite);

        GameManager.Instance?.IncrementChestsOpened();
        chestCounterUI?.IncrementChestCount();

        StartCoroutine(HandlePostChestOpen());
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
