using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    public AudioSource proximityAudio;
    public AudioSource openChestAudio;
    public float maxDistance = 10f;

    private void Update()
    {
        AdjustProximityAudioVolume();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayProximityAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopProximityAudio();
        }
    }

    private void PlayProximityAudio()
    {
        if (proximityAudio != null && !proximityAudio.isPlaying)
        {
            proximityAudio.loop = true;
            proximityAudio.Play();
        }
    }

    private void StopProximityAudio()
    {
        if (proximityAudio != null && proximityAudio.isPlaying)
        {
            proximityAudio.Stop();
        }
    }

    private void AdjustProximityAudioVolume()
    {
        if (proximityAudio != null)
        {
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            proximityAudio.volume = Mathf.Clamp01(1 - Mathf.InverseLerp(0, maxDistance, distance));
        }
    }

    // Llamado desde ChestController cuando se abra el cofre
    public void StopProximityAudioOnOpen()
    {
        StopProximityAudio();
    }

    // Llamado desde ChestController para reproducir el sonido de abrir el cofre
    public void PlayOpenChestAudio()
    {
        if (openChestAudio != null && !openChestAudio.isPlaying)
        {
            openChestAudio.Play();
        }
    }
}
