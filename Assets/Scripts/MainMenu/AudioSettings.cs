using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = 0.5f;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        SetVolume(0.5f);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }
}
