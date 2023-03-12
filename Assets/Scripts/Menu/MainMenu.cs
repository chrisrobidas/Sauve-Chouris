using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCanvas;

    [SerializeField]
    private GameObject levelSelectionCanvas;

    [SerializeField]
    private GameObject buttonsCanvas;

    [SerializeField]
    private GameObject rainCanvas;

    [SerializeField]
    private IntroCutscene introCutsceneScript;

    [SerializeField]
    private Button level1Button;

    [SerializeField]
    private Button level2Button;

    [SerializeField]
    private Button level3Button;

    private int selectedLevel;

    private Coroutine _cutsceneCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }
    }

    public void Play()
    {
        mainCanvas.SetActive(false);
        buttonsCanvas.SetActive(false);
        rainCanvas.SetActive(false);
        _cutsceneCoroutine = StartCoroutine(StartCutscene());
    }

    private IEnumerator StartCutscene()
    {
        yield return introCutsceneScript.ShowCutscene();
        ShowLevelSelectionCanvas();
    }

    private void SkipCutscene()
    {
        StopCoroutine(_cutsceneCoroutine);
        _cutsceneCoroutine = null;
        introCutsceneScript.ResetUI();
        ShowLevelSelectionCanvas();
    }

    private void ShowLevelSelectionCanvas()
    {
        levelSelectionCanvas.SetActive(true);
        level1Button.interactable = false;
        selectedLevel = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        levelSelectionCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        buttonsCanvas.SetActive(true);
        rainCanvas.SetActive(true);
    }

    public void ChangeLevel(int levelNumber)
    {
        selectedLevel = levelNumber;

        switch (levelNumber)
        {
            case 1:
                level1Button.interactable = false;
                level2Button.interactable = true;
                level3Button.interactable = true;
                break;
            case 2:
                level1Button.interactable = true;
                level2Button.interactable = false;
                level3Button.interactable = true;
                break;
            case 3:
                level1Button.interactable = true;
                level2Button.interactable = true;
                level3Button.interactable = false;
                break;
        }
    }

    public void StartLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level_" + selectedLevel);
    }
}
