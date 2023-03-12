using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // private GameManager _gameManager;
    [SerializeField] private GameObject gameOverPanel;
    private bool _isActive;

    public string ActiveLevel { get; set; }

    private void Start()
    {
        // _gameManager = FindObjectOfType<GameManager>();
        _isActive = false;
    }

    private void Update()
    {
        // if (_gameManager.GameIsEnded)
        // {
        //     
        // }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }  

    public void ToggleGameOverMenu()
    {
        _isActive = !_isActive;
        gameOverPanel.SetActive(_isActive);
        
        if (_isActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit() => Application.Quit();
}
