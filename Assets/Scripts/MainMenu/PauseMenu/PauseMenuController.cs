using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;


    private bool isPaused = false;

    private void Start()
    {
        pauseMenuCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

       

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    private void Tutorial()
    {
        Time.timeScale = 1f;
        GameManager.Instance.chestsOpened = 0;
        SceneManager.LoadScene("Tutorial");
    }

    public void Isle1()
    {
        Time.timeScale = 1f;
        GameManager.Instance.chestsOpened = 0;
        SceneManager.LoadScene("Isle1");
    }

    public void Isle2()
    {
        Time.timeScale = 1f;
        GameManager.Instance.chestsOpened = 1;
        SceneManager.LoadScene("Isle2");
    }

    public void Isle3()
    {
        Time.timeScale = 1f;
        GameManager.Instance.chestsOpened = 2;
        SceneManager.LoadScene("Isle3");
    }

    public void GetAllMemories()
    {
        GameManager.Instance.chestsOpened = 3;
    }

    public void GetAllMaps()
    {
        GameManager.Instance.mapsCollected = 100;
    }
}
