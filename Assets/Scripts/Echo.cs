using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Echo : MonoBehaviour
{
    private bool isActive = false;
    private bool isExpanding = false;
    private float speed = 2f;
    private float expandDuration = 2f;
    private float fadeInTime = 1f;
    private float fadeOutTime = 2f;
    private SpriteRenderer _echoSprite;
    private Revealable _revealableObject;
    private Light2D _light2D;
    private RevealableLight _revealableLight;

    private float _range = 0f;
    private float _elapsedTime = 0f;
    private List<Collider2D> _collidersHit = new List<Collider2D>();

    private void Start()
    {
        _echoSprite = GetComponent<SpriteRenderer>();
        _revealableObject = GetComponent<Revealable>();
        _light2D = GetComponent<Light2D>();
        _revealableLight = GetComponent<RevealableLight>();
    }

    public void SetValues(float speed, float expandDuration, float fadeInTime, float fadeOutTime, Sprite echoSprite)
    {
        this.speed = speed;
        this.expandDuration = expandDuration;
        this.fadeInTime = fadeInTime;
        this.fadeOutTime = fadeOutTime;
        _echoSprite.sprite = echoSprite;
        _revealableObject.SetFadeInFadeOut(fadeInTime, fadeOutTime);
        _revealableLight.SetFadeInFadeOut(fadeInTime, fadeOutTime);
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
            isExpanding = true;
            _revealableObject.Reveal();
            _revealableLight.Reveal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float canActivateTime = expandDuration > fadeInTime + fadeOutTime ? expandDuration : fadeInTime + fadeOutTime;
        if (_elapsedTime > expandDuration)
        {
            isExpanding = false;
        }

        if (_elapsedTime > canActivateTime)
        {
            isActive = false;
            _elapsedTime = 0f;
            _range = 0f;
            _collidersHit.Clear();
        }

        if (isExpanding)
        {
            _range += speed * Time.deltaTime;
            _echoSprite.transform.localScale = new Vector3(_range, _range);
            EchoLocalize(_range);
        }

        if (isActive)
        {
            _elapsedTime += Time.deltaTime;
            _light2D.pointLightOuterRadius = _range * 2.5f;
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
