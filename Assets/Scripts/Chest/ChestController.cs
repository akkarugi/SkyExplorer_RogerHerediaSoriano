using UnityEngine;
using UnityEngine.Playables;

public class ChestController : MonoBehaviour
{
    public PlayableDirector chestTimeline;
    public Collider canOpenCollider;
    public PlayerProximity playerProximity; 

    private bool isChestOpened = false;

    public void OpenChest()
    {
        if (!isChestOpened && canOpenCollider.bounds.Contains(Camera.main.transform.position))
        {
            chestTimeline.Play();
            isChestOpened = true;

            playerProximity.StopProximityAudioOnOpen();
            playerProximity.PlayOpenChestAudio();
            Destroy(gameObject, 5);
        }
    }

    public bool IsChestOpened() => isChestOpened;
}
