using UnityEngine;
using TMPro;

public class Map_Collectables : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioSource collectSound;
    private int score = 0;
    private bool isNear = false;

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            collectSound.Play();
            score++;
            scoreText.text = "Map pieces: " + score;
            Destroy(other.gameObject);
        }
       
    }

   
}
