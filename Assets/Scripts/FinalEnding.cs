using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalEnding : CutScene
{
    [SerializeField] private Image endingImage;
    private bool won;

    void Start()
    {
        ResetUI();
        won = false;
    }

    public void ResetUI()
    {
        endingImage.color = new Color(1, 1, 1, 0);
        endingImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trap triggered");
        // GetComponent<PlayerMovement>().enabled = false;
        // Time.timeScale = 0f;
        // won = true;
        // StartCoroutine(LevelTransition());
        ToggleWin();
    }

    private void ToggleWin()
    {
        if (won)
        {
            StartCoroutine(LevelTransition());
        }
        won = !won;
    }

    private IEnumerator LevelTransition()
    {
        endingImage.enabled = true;
        yield return ImageFadeTo(1.0f, 1, endingImage);
        yield return new WaitForSeconds(4);
        // yield return ImageFadeTo(0.0f, 1, endingImage);
        // GetComponent<PlayerMovement>().enabled = true;
        SceneManager.LoadScene("MainMenu");
    }
}