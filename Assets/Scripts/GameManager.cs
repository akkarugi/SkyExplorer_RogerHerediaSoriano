using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int chestsOpened = 0;
    public int mapsCollected = 0;

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
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Win")
        {
            chestsOpened = 0;
            mapsCollected = 0;
        }
    }

    public void IncrementChestsOpened() => chestsOpened++;
    public void IncrementMapsCollected() => mapsCollected++;
}
