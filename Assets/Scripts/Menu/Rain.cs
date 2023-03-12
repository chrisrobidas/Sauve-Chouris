using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField]
    private GameObject rain1Image;

    [SerializeField]
    private GameObject rain2Image;

    private float _elaspsedTime;

    private void Start()
    {
        rain1Image.SetActive(true);
        rain2Image.SetActive(false);
    }

    private void Update()
    {
        _elaspsedTime += Time.deltaTime;

        if (_elaspsedTime > 0.5f)
        {
            rain1Image.SetActive(!rain1Image.activeSelf);
            rain2Image.SetActive(!rain2Image.activeSelf);
            _elaspsedTime = 0;
        }
    }
}
