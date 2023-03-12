using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Echo : MonoBehaviour
{
    private bool _isActive = false;
    private bool _isExpanding = false;
    private float _speed = 2f;
    private float _expandDuration = 2f;
    private float _fadeInTime = 1f;
    private float _fadeOutTime = 2f;
    private SpriteRenderer _echoSprite;
    private Revealable _revealableObject;
    private Light2D _light2D;
    private RevealableLight _revealableLight;

    private float _range = 0f;
    private float _elapsedTime = 0f;
    private List<Collider2D> _collidersHit = new List<Collider2D>();

    private void Start()
    {
        _echoSprite = gameObject.AddComponent<SpriteRenderer>();
        _revealableObject = gameObject.AddComponent<Revealable>();
        _light2D = gameObject.AddComponent<Light2D>();
        _revealableLight = gameObject.AddComponent<RevealableLight>();
    }

    public void SetValues(float speed, float expandDuration, float fadeInTime, float fadeOutTime, Sprite echoSprite)
    {
        _speed = speed;
        _expandDuration = expandDuration;
        _fadeInTime = fadeInTime;
        _fadeOutTime = fadeOutTime;
        _echoSprite.sprite = echoSprite;
        _revealableObject.SetFadeInFadeOut(fadeInTime, fadeOutTime);
        _revealableLight.SetFadeInFadeOut(fadeInTime, fadeOutTime);
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            _isExpanding = true;
            _revealableObject.Reveal();
            _revealableLight.Reveal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float canActivateTime = _expandDuration > _fadeInTime + _fadeOutTime ? _expandDuration : _fadeInTime + _fadeOutTime;
        if (_elapsedTime > _expandDuration)
        {
            _isExpanding = false;
        }

        if (_elapsedTime > canActivateTime)
        {
            _isActive = false;
            _elapsedTime = 0f;
            _range = 0f;
            _collidersHit.Clear();
        }

        if (_isExpanding)
        {
            _range += _speed * Time.deltaTime;
            _echoSprite.transform.localScale = new Vector3(_range, _range);
            EchoLocalize(_range);
        }

        if (_isActive)
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
