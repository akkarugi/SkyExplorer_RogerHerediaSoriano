using UnityEngine;
using TMPro;

public class ChestCounterUI : MonoBehaviour
{
    public TextMeshProUGUI chestsOpenedText;
    

    private void Start()
    {
        UpdateChestsOpenedText();
    }

    public void IncrementChestCount()
    {
        GameManager.Instance.chestsOpened++;
        UpdateChestsOpenedText();
    }

    private void UpdateChestsOpenedText()
    {
        if (chestsOpenedText != null)
        {
            chestsOpenedText.text = "Memories: " + GameManager.Instance.chestsOpened.ToString() + " / 3";
        }
    }
}
