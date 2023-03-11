using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class Echolocation : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float duration = 2f;
    [SerializeField]
    private float fadeInTime = 1f;
    [SerializeField]
    private float fadeOutTime = 2f;
    [SerializeField]
    private Sprite _echoSprite;

    GameObject echo;
    Echo echoComponent;
    [SerializeField]
    private GameObject spotlight2D;

    private void Start()
    {
        echo = new GameObject();
        echo.AddComponent<SpriteRenderer>();
        echo.AddComponent<Revealable>();
        echo.AddComponent<Light2D>();
        echo.AddComponent<RevealableLight>();
        echoComponent = echo.AddComponent<Echo>();
        Instantiate(echo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !echoComponent.IsActive())
        {
            echo.transform.position = transform.position;
            echoComponent.SetValues(speed, duration, fadeInTime, fadeOutTime, _echoSprite);
            echoComponent.Activate();
        }
    }
}
