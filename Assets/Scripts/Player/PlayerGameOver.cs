using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
    