using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;

    private bool isPaused = false;

    private void Start()
    {
        // Desactivar el Canvas y ocultar el cursor al inicio
        pauseMenuCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor al centro de la pantalla
    }

    private void Update()
    {
        // Detectar la tecla Escape para pausar/despausar
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
        Time.timeScale = 0f; // Pausa el tiempo
        pauseMenuCanvas.SetActive(true); // Activa el Canvas del menú de pausa

        // Mostrar el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Liberar el cursor
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Reanuda el tiempo
        pauseMenuCanvas.SetActive(false); // Desactiva el Canvas del menú de pausa

        // Ocultar el cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor al centro de la pantalla
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté reanudado
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }
}
