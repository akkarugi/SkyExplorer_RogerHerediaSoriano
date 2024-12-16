using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleManager : MonoBehaviour
{
    private int totalCircles;
    private int circlesCleared = 0;

    void Start()
    {
        totalCircles = GameObject.FindGameObjectsWithTag("Circle").Length;
    }

    void Update()
    {
        if (circlesCleared >= totalCircles)
        {
            ChangeScene();
        }
    }

    public void CircleCleared()
    {
        circlesCleared++;
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Level");
    }
}
