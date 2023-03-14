using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnding : CutScene
{
    [SerializeField] private Image[] endingImages;
    [SerializeField] private int nextLevel;

    private SoundManager _soundManagerScript;

    void Start()
    {
        ResetUI();
        _soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void ResetUI()
    {
        foreach (var img in endingImages)
        {
            img.color = new Color(1, 1, 1, 0);
            img.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetInt("IsLevel" + nextLevel + "Unlocked", 1);
        PlayerPrefs.Save();
        _soundManagerScript.PlaySound("Sauve_Chouris");
        StartCoroutine(LevelTransition());
    }


    private IEnumerator LevelTransition()
    {
        yield return ShowSlide(endingImages[0], 1);
        yield return ShowSlide(endingImages[1], 0);
        yield return ShowSlide(endingImages[2], 0);
        
        
        yield return ShowSlide(endingImages[0], 0);
        yield return ShowSlide(endingImages[1], 0);
        yield return ShowSlide(endingImages[2], 0);
        
        
        yield return ShowSlide(endingImages[0], 0);
        yield return ShowSlide(endingImages[1], 0);
        
        yield return ShowSlide(endingImages[2], 2);

        AkSoundEngine.StopAll();
        SceneManager.LoadSceneAsync("Level_" + nextLevel);
    }

    private IEnumerator ShowSlide(Image img, int fade)
    {
        img.enabled = true;
        if (fade == 1)
        {
            yield return ImageFadeTo(1.0f, 1, img);
        } 
        
        img.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        img.color = new Color(1, 1, 1, 0);
        
        if (fade == 2)
        {
            yield return ImageFadeTo(0.0f, 1, img);
        }
        img.enabled = false;
    }
}
