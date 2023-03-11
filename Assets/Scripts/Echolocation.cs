using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocation : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float duration = 2f;
    [SerializeField]
    private Sprite _echoSprite;

    GameObject echo;
    Echo echoComponent;

    private void Start()
    {
        echo = new GameObject();
        echo.AddComponent<SpriteRenderer>();
        echo.AddComponent<Revealable>();
        echoComponent = echo.AddComponent<Echo>();
        Instantiate(echo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !echoComponent.IsActive())
        {
            echo.transform.position = transform.position;
            echoComponent.SetValues(speed, duration, _echoSprite);
            echoComponent.Activate();
        }
    }
}
