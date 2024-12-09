using UnityEngine;
using TMPro;

public class Map_Collectables : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioSource collectSound;
    
 

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            collectSound.Play();
            GameManager.Instance.mapsCollected++;
            scoreText.text = "Map pieces: " + GameManager.Instance.mapsCollected + " / ?";
            Destroy(other.gameObject);
        }
       
    }

   
}
