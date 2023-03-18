using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    public bool _isPaused;

    private SoundManager _soundManagerScript;

    private void Start()
    {
        _soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        _isPaused = !_isPaused;
        pausePanel.SetActive(_isPaused);

        if (_isPaused)
        {
            _soundManagerScript.StopMusicEvent();
            Time.timeScale = 0f;
        }
        else
        {
            _soundManagerScript.StartMusicEvent();
            Time.timeScale = 1f;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
