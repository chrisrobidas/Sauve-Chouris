using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revealable : MonoBehaviour
{
    public float FadeInTime = 1.0f;
    public float FadeOutTime = 1.5f;

    private Renderer _renderer;
    
    private void Start()
    {
        _renderer = transform.GetComponent<Renderer>();
        Color invisible = new Color(1, 1, 1, 0);
        _renderer.material.color = invisible;
    }

    public void SetDuration(float duration)
    {
        FadeInTime = duration / 3f;
        FadeOutTime = 2f * (duration / 3f);
    }

    public void SetFadeInFadeOut(float fadeInTime, float fadeOutTime)
    {
        FadeInTime = fadeInTime;
        FadeOutTime = fadeOutTime;
    }

    public void Reveal()
    {
        StartCoroutine(FadeInThenOut());
    }

    private IEnumerator FadeInThenOut()
    {
        yield return FadeTo(1.0f, FadeInTime);
        yield return FadeTo(0.0f, FadeOutTime);
    }

    private IEnumerator FadeTo(float value, float time)
    {
        float alpha = _renderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, value, t));
            _renderer.material.color = newColor;
            yield return null;
        }
    }
}
