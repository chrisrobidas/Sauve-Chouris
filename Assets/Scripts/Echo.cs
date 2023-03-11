using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    private bool isActive = false;
    private float speed = 2f;
    private float duration = 2f;
    private SpriteRenderer _echoSprite;
    private Revealable _echoRevealableScript;

    private float _range = 0f;
    private float _elapsedTime = 0f;
    private List<Collider2D> _collidersHit = new List<Collider2D>();

    private void Start()
    {
        _echoSprite = GetComponent<SpriteRenderer>();
        _echoRevealableScript = GetComponent<Revealable>();
    }

    public void SetValues(float speed, float duration, Sprite echoSprite)
    {
        this.speed = speed;
        this.duration = duration;
        _echoSprite.sprite = echoSprite;
        _echoRevealableScript.SetDuration(duration);
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            _echoRevealableScript.Reveal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime > duration)
        {
            isActive = false;
            _range = 0f;
            _elapsedTime = 0f;
            _collidersHit.Clear();
        }

        if (isActive)
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
