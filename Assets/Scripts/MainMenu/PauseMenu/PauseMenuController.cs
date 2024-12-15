using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;

    [Header("Isle Respawn Settings")]
    [SerializeField] private Transform isle1RespawnPoint;
    [SerializeField] private Transform isle2RespawnPoint;
    [SerializeField] private Transform isle3RespawnPoint;

   

    [SerializeField] private Canvas[] activeCanvases;

    private Transform player;
    private bool isPaused = false;

    private void Start()
    {
        pauseMenuCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
        

        foreach (Canvas canvas in activeCanvases)
        {
            if (canvas != null && canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(false);
            }
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);


        foreach (Canvas canvas in activeCanvases)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
      
        foreach (Canvas canvas in activeCanvases)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    public void Isle1()
    {
        RespawnPlayer(isle1RespawnPoint.position);
    }

    public void Isle2()
    {
        RespawnPlayer(isle2RespawnPoint.position);
        GameManager.Instance.chestsOpened = 1;
    }

    public void Isle3()
    {
        RespawnPlayer(isle3RespawnPoint.position);
        GameManager.Instance.chestsOpened = 2;
    }

    private void RespawnPlayer(Vector3 respawnPosition)
    {
        if (player != null)
        {
            player.position = respawnPosition;
        }
    }
}
