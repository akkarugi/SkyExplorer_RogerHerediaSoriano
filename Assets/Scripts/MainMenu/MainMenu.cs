using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayGame()
    {
   
        UnityEngine.SceneManagement.SceneManager.LoadScene("Isle1");
        GameManager.Instance.mapsCollected = 0;
        GameManager.Instance.chestsOpened = 0;

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
