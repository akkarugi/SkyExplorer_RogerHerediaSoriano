using UnityEngine;

public class Circle : MonoBehaviour
{
    public CircleManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.CircleCleared();
            gameObject.SetActive(false);
        }
    }
}
