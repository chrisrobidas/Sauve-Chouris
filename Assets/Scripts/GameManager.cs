using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool GameIsEnded { get; set; }
    private GameOverMenu _gameOverMenu;

    private void Start()
    {
        GameIsEnded = false;
        _gameOverMenu = FindObjectOfType<GameOverMenu>();
    }

    public void EndGame()
    {
        if (!GameIsEnded)
        {
            GameIsEnded = true;
            _gameOverMenu.ActiveLevel = SceneManager.GetActiveScene().name;
            _gameOverMenu.ToggleGameOverMenu();
        }
    }
}
