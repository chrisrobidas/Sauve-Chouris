using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCanvas;

    [SerializeField]
    private GameObject levelSelectionCanvas;

    [SerializeField]
    private TMP_Text levelText;

    private int selectedLevel;

    public void Play()
    {
        mainCanvas.SetActive(false);
        levelSelectionCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        levelSelectionCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void ChangeLevel(int levelNumber)
    {
        selectedLevel = levelNumber;
        levelText.text = "NIVEAU " + levelNumber.ToString();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level_" + selectedLevel);
    }
}
