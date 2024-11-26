using UnityEngine;
using UnityEngine.Playables;

public class ChestController : MonoBehaviour
{
    public PlayableDirector chestTimeline;
    public Collider canOpenCollider;
    public PlayerProximity playerProximity; // Referencia al script PlayerProximity

    private bool isChestOpened = false;

    public void OpenChest()
    {
        if (!isChestOpened && canOpenCollider.bounds.Contains(Camera.main.transform.position))
        {
            chestTimeline.Play();
            isChestOpened = true;

            // Detener el sonido de proximidad y reproducir el sonido de abrir el cofre
            playerProximity.StopProximityAudioOnOpen();
            playerProximity.PlayOpenChestAudio();
        }
    }

    public bool IsChestOpened() => isChestOpened;
}
