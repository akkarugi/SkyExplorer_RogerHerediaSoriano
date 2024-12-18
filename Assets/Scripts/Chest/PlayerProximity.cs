using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    public AudioSource proximityAudio;
    public AudioSource openChestAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (proximityAudio != null && !proximityAudio.isPlaying)
            {
                proximityAudio.loop = true;
                proximityAudio.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (proximityAudio != null && proximityAudio.isPlaying)
            {
                proximityAudio.Stop();
            }
        }
    }

    public void StopProximityAudioOnOpen()
    {
        if (proximityAudio != null && proximityAudio.isPlaying)
        {
            proximityAudio.Stop();
        }
    }

    public void PlayOpenChestAudio()
    {
        if (openChestAudio != null && !openChestAudio.isPlaying)
        {
            openChestAudio.Play();
        }
    }
}
