using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu; // Contenedor de los botones Play, Options, Exit
    public GameObject optionsMenu; // Contenedor del menú de opciones

    private void Start()
    {
        // Asegurarse de que los menús estén configurados al iniciar la escena
        if (mainMenu != null) mainMenu.SetActive(true);
        if (optionsMenu != null) optionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    public void OpenOptions()
    {
        if (mainMenu != null && optionsMenu != null)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (mainMenu != null && optionsMenu != null)
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
