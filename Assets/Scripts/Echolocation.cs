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
    private Sprite echoSprite;

    public float EchoCooldownTime = 6f;
    public float EchoRemainingCooldownTime = 0f;

    private GameObject _echo;
    private Echo _echoComponent;

    private void Start()
    {
        _echo = new GameObject();
        _echoComponent = _echo.AddComponent<Echo>();
        Instantiate(_echo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_echoComponent.IsActive() && EchoRemainingCooldownTime <= 0f)
        {
            EchoRemainingCooldownTime = EchoCooldownTime;
            _echo.transform.position = transform.position;
            _echoComponent.SetValues(speed, expandDuration, fadeInTime, fadeOutTime, echoSprite);
            _echoComponent.Activate();
        }

        if (EchoRemainingCooldownTime > 0f)
        {
            EchoRemainingCooldownTime -= Time.deltaTime;
        } 
    }
}
