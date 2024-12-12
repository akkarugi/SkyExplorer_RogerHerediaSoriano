using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject volumeMenu;

    private bool isPaused = false;
    private AudioSource[] allAudioSources;

    // Lista para guardar los canvas activos
    private List<GameObject> activeCanvases = new List<GameObject>();

    private void Start()
    {
        pauseMenuCanvas.SetActive(false);
        optionsMenu.SetActive(false);
        volumeMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        allAudioSources = FindObjectsOfType<AudioSource>();
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
                Open3OptionsMenu();
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

        // Guardar los canvases activos
        activeCanvases.Clear();
        foreach (var canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.gameObject.activeInHierarchy && canvas.gameObject != pauseMenuCanvas)
            {
                activeCanvases.Add(canvas.gameObject);
                canvas.gameObject.SetActive(false); // Desactivar el canvas
            }
        }

        foreach (var audioSource in allAudioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        optionsMenu.SetActive(false);
        volumeMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Restaurar los canvases activos
        foreach (var canvas in activeCanvases)
        {
            canvas.SetActive(true); // Activar el canvas guardado
        }

        foreach (var audioSource in allAudioSources)
        {
            audioSource.UnPause();
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenOptionsMenu()
    {
        if (pauseMenuCanvas != null && optionsMenu != null)
        {
            pauseMenuCanvas.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    public void OpenVolumeMenu()
    {
        if (optionsMenu != null && volumeMenu != null)
        {
            optionsMenu.SetActive(false);
            volumeMenu.SetActive(true);
        }
    }

    public void CloseVolumeMenu()
    {
        if (optionsMenu != null && volumeMenu != null)
        {
            volumeMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    private void Open3OptionsMenu()
    {
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(true);
        }
    }
}
