using UnityEngine;

public class Item : MonoBehaviour
{
    public int points = 100; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Players")) 
        {
            ScoreManager.instance.AddScore(points);

            Destroy(gameObject);
        }
    }
}