using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;  // Slider para controlar el volumen

    private void Start()
    {
        // Cargar el volumen guardado (si existe), de lo contrario, asignar 0.5 como valor predeterminado
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        AudioListener.volume = savedVolume;

        // Inicializar el slider con el valor guardado
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Método para ajustar el volumen del AudioListener
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        // Guardar el volumen en PlayerPrefs para que persista entre sesiones
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Asegúrate de eliminar el listener cuando el objeto se destruya
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }
}
