using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameIsEnded { get; set; }
    private GameOverMenu _gameOverMenu;

    private void Awake()
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
            // Restart();
        }
    }

    // private void Restart() =>  SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
}
