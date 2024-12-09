using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int chestsOpened = 0;
    public int mapsCollected = 0;

    private bool isChangingScene = false;
    private float timer = 0f;
    private float delay = 5f; // Tiempo de espera antes de cambiar de escena

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (chestsOpened >= 3 && !isChangingScene)
        {
            isChangingScene = true;
        }

        if (isChangingScene)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
