using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    private SoundManager _soundManagerScript;

    private void Start()
    {
        _soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _soundManagerScript.PlaySound("Death");
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
    