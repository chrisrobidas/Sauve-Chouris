using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalEnding : CutScene
{
    [SerializeField] private Image endingImage;
    private bool won;

    [SerializeField]
    TMP_Text intro4Text;

    private SoundManager _soundManagerScript;

    void Start()
    {
        ResetUI();
        won = false;
        _soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void ResetUI()
    {
        endingImage.color = new Color(1, 1, 1, 0);
        endingImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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
        _soundManagerScript.PlaySound("Sauve_Chouris");
        yield return ImageFadeTo(1.0f, 1, endingImage);
        intro4Text.enabled = true;
        yield return new WaitForSeconds(4);
        intro4Text.enabled = false;
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("MainMenu");
    }
}