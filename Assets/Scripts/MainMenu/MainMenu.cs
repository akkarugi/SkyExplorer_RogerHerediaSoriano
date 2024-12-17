using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Isle1");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }
    public void Tutorial()
    {
    


        SceneManager.LoadScene("Tutorial");
    }


}
