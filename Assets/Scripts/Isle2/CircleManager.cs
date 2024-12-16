using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleManager : MonoBehaviour
{
    public GameObject[] circles;
    private int circlesCleared = 0;

    void Update()
    {
        if (circlesCleared >= circles.Length)
        {
            SceneManager.LoadScene("Level");
        }
    }

    public void CircleCleared()
    {
        circlesCleared++;
    }
}
