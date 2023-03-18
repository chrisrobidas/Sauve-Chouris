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
    private GameObject loadingCanvas;

    [SerializeField]
    private GameObject buttonsCanvas;

    [SerializeField]
    private GameObject rainCanvas;

    [SerializeField]
    private GameObject creditsCanvas;

    [SerializeField]
    private IntroCutscene introCutsceneScript;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button level1Button;

    [SerializeField]
    private Button level2Button;

    [SerializeField]
    private Button level3Button;

    [SerializeField]
    private GameObject level2Lock;

    [SerializeField]
    private GameObject level3Lock;

    private int selectedLevel;
    private int _isLevel2Unlocked;
    private int _isLevel3Unlocked;

    private bool _canSkipCutscene;
    private Coroutine _loadLevelCoroutine;

    private void Start()
    {
        loadingCanvas.SetActive(false);
        startButton.interactable = false;

        _isLevel2Unlocked = PlayerPrefs.GetInt("IsLevel2Unlocked", 0);
        _isLevel3Unlocked = PlayerPrefs.GetInt("IsLevel3Unlocked", 0);
        level2Button.enabled = _isLevel2Unlocked == 0 ? false : true;
        level3Button.enabled = _isLevel3Unlocked == 0 ? false : true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _canSkipCutscene)
        {
            introCutsceneScript.ResetUI();
            StopCoroutine(_loadLevelCoroutine);
            AkSoundEngine.StopAll();
            SceneManager.LoadScene("Level_1");
        }
    }

    public void Play()
    {
        mainCanvas.SetActive(false);
        buttonsCanvas.SetActive(false);
        rainCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        ShowLevelSelectionCanvas();
    }

    public void Credits()
    {
        creditsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        buttonsCanvas.SetActive(false);
        rainCanvas.SetActive(false);
        levelSelectionCanvas.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        levelSelectionCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        buttonsCanvas.SetActive(true);
        rainCanvas.SetActive(true);
        startButton.interactable = false;
        level1Button.interactable = true;
        level2Button.interactable = true;
        level3Button.interactable = true;
        selectedLevel = 0;
    }

    private void ShowLevelSelectionCanvas()
    {
        levelSelectionCanvas.SetActive(true);
        startButton.interactable = false;

        level2Lock.SetActive(_isLevel2Unlocked == 0 ? true : false);
        level3Lock.SetActive(_isLevel3Unlocked == 0 ? true : false);
    }

    public void ChangeLevel(int levelNumber)
    {
        startButton.interactable = true;
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
        _loadLevelCoroutine = StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        loadingCanvas.SetActive(true);
        yield return new WaitForSeconds(2);

        if (selectedLevel == 1)
        {
            _canSkipCutscene = true;
            levelSelectionCanvas.SetActive(false);
            yield return introCutsceneScript.ShowCutscene();
        }

        AkSoundEngine.StopAll();
        SceneManager.LoadSceneAsync("Level_" + selectedLevel);
    }
}
