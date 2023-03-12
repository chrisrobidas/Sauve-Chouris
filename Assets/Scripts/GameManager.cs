using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameIsEnded = false;

    public void EndGame()
    {
        
        if (!_gameIsEnded)
        {
            _gameIsEnded = true;
            Restart();
        }
    }

    private void Restart() =>  SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
}
