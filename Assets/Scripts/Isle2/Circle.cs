using UnityEngine;

public class Circle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
           
            FindObjectOfType<CircleManager>().CircleCleared();

         
            gameObject.SetActive(false);
        }
    }
}
