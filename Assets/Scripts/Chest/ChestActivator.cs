using UnityEngine;

public class ChestActivator : MonoBehaviour
{
    public GameObject openButtonSprite;
    public Transform player;
    public Collider canOpenCollider;
    public ChestController chestController;
    public GameObject canvas;
    public ChestCounterUI chestCounterUI;
    public FuelSystem fuelSystem; // Referencia al sistema de combustible

    private bool hasOpenedChest = false;

    private void Start()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

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
                chestCounterUI?.IncrementChestCount();
                fuelSystem.RefillFuel(); 
                StartCoroutine(ShowCanvasAfterDelay(2f));
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
        if (sprite != null)
        {
            Vector3 direction = (player.position - sprite.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 180, 0);
            sprite.transform.rotation = lookRotation;
        }
    }

    private System.Collections.IEnumerator ShowCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }
}
