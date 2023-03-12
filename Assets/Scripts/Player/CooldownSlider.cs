using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CooldownSlider : MonoBehaviour
{
    [SerializeField]
    private Echolocation playerEchoLocation;

    [SerializeField]
    private Image sliderFill;

    [SerializeField]
    private Image sliderBackground;

    private Slider _slider;
    private Coroutine _fadeOutCoroutine;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        sliderFill.color = new Color(1, 1, 1, 0);
        sliderBackground.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        _slider.value = (playerEchoLocation.EchoCooldownTime - playerEchoLocation.EchoRemainingCooldownTime) / playerEchoLocation.EchoCooldownTime;

        if (_slider.value >= 0.99 && sliderFill.color.a >= 0.99 && _fadeOutCoroutine == null)
        {
            _fadeOutCoroutine = StartCoroutine(ImageFadeTo(0, 1));
        }

        if (_slider.value <= 0.01 && sliderFill.color.a <= 0.01)
        {
            StartCoroutine(ImageFadeTo(1, 1));
            _fadeOutCoroutine = null;
        }
    }

    public void StopFadeOut()
    {
        if (_fadeOutCoroutine != null)
        {
            Debug.Log("Stopping coroutine");
            StopCoroutine(_fadeOutCoroutine);
            _fadeOutCoroutine = null;
        }
    }

    private IEnumerator ImageFadeTo(float value, float time)
    {
        float alphaSliderFill = sliderFill.color.a;
        float alphaSliderBackground = sliderBackground.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColorSliderFill = new Color(1, 1, 1, Mathf.Lerp(alphaSliderFill, value, t));
            sliderFill.color = newColorSliderFill;

            Color newColorSliderBackground = new Color(1, 1, 1, Mathf.Lerp(alphaSliderBackground, value, t));
            sliderBackground.color = newColorSliderBackground;

            yield return null;
        }
    }
}
