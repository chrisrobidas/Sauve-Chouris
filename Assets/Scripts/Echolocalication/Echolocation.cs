using UnityEngine;

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
    private SoundManager _soundManagerScript;
    private CooldownSlider _cooldownSliderScript;

    private void Start()
    {
        _soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _cooldownSliderScript = GameObject.Find("CooldownSlider").GetComponent<CooldownSlider>();

        _echo = new GameObject();
        _echoComponent = _echo.AddComponent<Echo>();
        Instantiate(_echo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_echoComponent.IsActive() && EchoRemainingCooldownTime <= 0f)
        {
            _cooldownSliderScript.StopFadeOut();
            _soundManagerScript.PlaySound("Echo_Shoot");
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
