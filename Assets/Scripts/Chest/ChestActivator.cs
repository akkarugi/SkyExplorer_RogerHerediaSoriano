using UnityEngine;

public class ChestActivator : MonoBehaviour
{
    public GameObject openButtonSprite;
    public Transform player;
    public Collider canOpenCollider;
    public ChestController chestController;

    private bool hasOpenedChest = false;

    private void Update()
    {
        if (hasOpenedChest) return;

        if (IsPlayerInCanOpenZone())
        {
            ShowOpenButton();
            LookAtPlayer(openButtonSprite);

            if (Input.GetKeyDown(KeyCode.E))
            {
                chestController.OpenChest();
                Destroy(openButtonSprite);
                hasOpenedChest = true;
            }
        }
        else
        {
            HideOpenButton();
        }
    }

    private bool IsPlayerInCanOpenZone() => canOpenCollider.bounds.Contains(player.position);

    private void ShowOpenButton()
    {
        if (openButtonSprite != null)
        {
            openButtonSprite.SetActive(true);
        }
    }

    private void HideOpenButton()
    {
        if (openButtonSprite != null)
        {
            openButtonSprite.SetActive(false);
        }
    }

    private void LookAtPlayer(GameObject sprite)
    {
        Vector3 direction = (player.position - sprite.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 180, 0); // Mirar al jugador hacia la derecha
        sprite.transform.rotation = lookRotation;
    }
}
