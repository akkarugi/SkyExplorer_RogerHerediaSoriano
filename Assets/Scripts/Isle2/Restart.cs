using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject canvasToHide;

    private void Start()
    {
        Invoke("HideCanvas", 6f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("FlightLevel");
        }
    }

    private void HideCanvas()
    {
        if (canvasToHide != null)
        {
            canvasToHide.SetActive(false);
        }
    }
}
