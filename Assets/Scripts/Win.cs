using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
   
    public string sceneToLoadOnExit = "MainMenu"; 
    public string sceneToLoadOnViewMap = " "; 

  
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(sceneToLoadOnExit);
    }


    public void LoadMap()
    {
        SceneManager.LoadScene(sceneToLoadOnViewMap);
    }
}
