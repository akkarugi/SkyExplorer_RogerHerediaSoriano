using UnityEngine;
using UnityEngine.UI;

public class UIElementInitializer : MonoBehaviour
{
    public enum UIElementType
    {
        SFX_Slider,
        MUSIC_Slider
    }

    public UIElementType type;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("No se encontró un componente Slider en el objeto.");
            return;
        }

        if (AudioSettings.audioSettings == null)
        {
            Debug.LogWarning("AudioSettings.audioSettings no está configurado.");
            return;
        }

        switch (type)
        {
            case UIElementType.SFX_Slider:
                slider.value = AudioSettings.audioSettings.GetSFXVolume();
                break;
            case UIElementType.MUSIC_Slider:
                slider.value = AudioSettings.audioSettings.GetMusicVolume();
                break;
        }
    }
}
