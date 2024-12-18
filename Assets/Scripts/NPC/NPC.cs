using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject eKeySprite;
    public GameObject dialogueCanvas;
    public Transform player;

    private bool isPlayerInRange;

    private void Start()
    {
        eKeySprite?.SetActive(false);
        dialogueCanvas?.SetActive(false);
    }

    private void Update()
    {
        if (!isPlayerInRange) return;

        RotateSpriteTowardsPlayer(eKeySprite);

        if (Input.GetKeyDown(KeyCode.E))
        {
            eKeySprite?.SetActive(false);
            dialogueCanvas?.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            eKeySprite?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            eKeySprite?.SetActive(false);
            dialogueCanvas?.SetActive(false);
        }
    }

    private void RotateSpriteTowardsPlayer(GameObject sprite)
    {
        if (sprite == null || player == null) return;

        Vector3 direction = (player.position - sprite.transform.position).normalized;
        direction.y = 0;
        sprite.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
    }
}
