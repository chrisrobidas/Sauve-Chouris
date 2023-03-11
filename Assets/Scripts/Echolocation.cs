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
    private GameObject echo;

    private SpriteRenderer _echoSprite;
    private Revealable _echoRevealableScript;

    private float _range = 0f;
    private float _elapsedTime = 0f;
    private bool _echoActive = false;
    private List<Collider2D> _collidersHit = new List<Collider2D> ();

    private void Start()
    {
        _echoSprite = echo.GetComponent<SpriteRenderer>();
        _echoRevealableScript = echo.GetComponent<Revealable>();
        _echoRevealableScript.FadeInTime = duration / 2;
        _echoRevealableScript.FadeOutTime = duration / 2;
    }

    private void Update()
    {
        if (_elapsedTime > duration)
        {
            _echoActive = false;
            _range = 0f;
            _elapsedTime = 0f;
            _collidersHit.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_echoActive)
        {
            _echoActive = true;
            _echoRevealableScript.Reveal();
        }

        if (_echoActive)
        {
            _elapsedTime += Time.deltaTime;
            _range += speed * Time.deltaTime;
            _echoSprite.transform.localScale = new Vector3(_range, _range);
            EchoLocalize(_range);
        }
    }

    public void EchoLocalize(float radius)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius * 2f, new Vector2(0, 0));
        foreach (RaycastHit2D hit in hits)
        {
            if (!_collidersHit.Contains(hit.collider))
            {
                _collidersHit.Add(hit.collider);
                Debug.Log("Hit object at x: " + hit.transform.position.x + ", y: " + hit.transform.position.y);

                // Reveal Revealable objects
                Revealable revealableScript = hit.transform.GetComponent<Revealable>();
                if (revealableScript != null)
                {
                    revealableScript.Reveal();
                }
            }
        }
    }
}
