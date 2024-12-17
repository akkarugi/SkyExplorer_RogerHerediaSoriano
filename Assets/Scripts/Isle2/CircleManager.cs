using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleManager : MonoBehaviour
{
    public GameObject[] circles;
    private int circlesCleared = 0;
    public AudioSource circleClearedSound; 

    void Update()
    {
        if (circlesCleared >= circles.Length)
        {
            SceneManager.LoadScene("Isle2");
        }
    }

    public void CircleCleared()
    {
        circlesCleared++;

       
        if (circleClearedSound != null)
        {
            circleClearedSound.Play();
        }
    }
}
