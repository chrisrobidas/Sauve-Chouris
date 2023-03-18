using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private bool _isActive;

    public string ActiveLevel { get; set; }

    private SoundManager _soundManagerScript;

    private void Start()
    {
        _soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _isActive = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }  

    public void ToggleGameOverMenu()
    {
        _isActive = !_isActive;
        gameOverPanel.SetActive(_isActive);
        
        if (_isActive)
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

    public void Exit() => Application.Quit();
}
