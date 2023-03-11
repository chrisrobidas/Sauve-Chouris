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
    private float expandDuration = 2f;
    [SerializeField]
    private float fadeInTime = 1f;
    [SerializeField]
    private float fadeOutTime = 2f;
    [SerializeField]
    private float echoCooldownTime = 6f;
    [SerializeField]
    private Sprite _echoSprite;

    float echoRemainingCooldownTime = 0f;
    GameObject echo;
    Echo echoComponent;

    private void Start()
    {
        echo = new GameObject();
        echoComponent = echo.AddComponent<Echo>();
        Instantiate(echo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !echoComponent.IsActive() && echoRemainingCooldownTime <= 0f)
        {
            echoRemainingCooldownTime = echoCooldownTime;
            echo.transform.position = transform.position;
            echoComponent.SetValues(speed, expandDuration, fadeInTime, fadeOutTime, _echoSprite);
            echoComponent.Activate();
        }

        if (echoRemainingCooldownTime > 0f)
        {
            echoRemainingCooldownTime -= Time.deltaTime;
        } 
    }
}
