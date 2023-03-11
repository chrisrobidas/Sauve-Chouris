using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RevealableLight : MonoBehaviour
{
    public float FadeInTime = 1.0f;
    public float FadeOutTime = 1.5f;

    private Light2D _spotlight2D;

    private void Start()
    {
        _spotlight2D = transform.GetComponent<Light2D>();
        _spotlight2D.intensity = 0;
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
        float initialIntensity = _spotlight2D.intensity;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            _spotlight2D.intensity = Mathf.Lerp(initialIntensity, value, t);
            Debug.Log(_spotlight2D.intensity);
            yield return null;
        }
    }
}
