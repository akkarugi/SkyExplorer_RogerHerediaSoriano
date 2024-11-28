using UnityEngine;
using TMPro;

public class Map_Collectables : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
         
            score++;
         
            scoreText.text = "Map pieces: " + score;
            // Destruir el cubo recolectado
            Destroy(other.gameObject);
        }
    }
}
