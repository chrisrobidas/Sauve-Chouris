using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnding : CutScene
{
    [SerializeField] private Image endingImage;

    [SerializeField] private TMP_Text endingText;
    
    void Start()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        endingImage.color = new Color(1, 1, 1, 0);
        endingImage.enabled = false;
        endingText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trap triggered");
        StartCoroutine(LevelTransition());
    }

    private IEnumerator LevelTransition()
    {
        Debug.Log("LevelTransition");
        endingImage.enabled = true;
        yield return ImageFadeTo(1.0f, 1, endingImage);
        endingText.enabled = true;
        yield return new WaitForSeconds(10);
        endingText.enabled = false;
        yield return ImageFadeTo(0.0f, 1, endingImage);
        endingText.enabled = false;
    }
}
