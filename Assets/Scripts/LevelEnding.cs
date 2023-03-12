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

    void Start()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        foreach (var img in endingImages)
        {
            img.color = new Color(1, 1, 1, 0);
            img.enabled = false;
        }
        // endingImage.color = new Color(1, 1, 1, 0);
        // endingImage.enabled = false;
        // endingText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trap triggered");
        
        StartCoroutine(LevelTransition());
        // endingImages[0].enabled = true;
        // yield return new WaitForSeconds(10);

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
        
        
        Debug.Log("loaddd");
        SceneManager.LoadScene("MainMenu");
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
