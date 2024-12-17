using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int chestsOpened = 0;
    public int mapsCollected = 0;

    private bool isChangingScene = false;
    private float delay = 5f;

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
            StartCoroutine(LoadWinSceneAfterDelay());
        }
    }

    private IEnumerator LoadWinSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Win");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
