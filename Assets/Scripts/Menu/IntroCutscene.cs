using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : CutScene
{
    [SerializeField]
    Image intro1Image;

    [SerializeField]
    Image intro2Image;

    [SerializeField]
    Image intro3Image;

    [SerializeField]
    Image intro4Image;

    [SerializeField]
    TMP_Text intro1Text;

    [SerializeField]
    TMP_Text intro2Text;

    [SerializeField]
    TMP_Text intro3Text;

    [SerializeField]
    TMP_Text intro4Text;

    private void Start()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        intro1Image.color = new Color(1, 1, 1, 0);
        intro1Image.enabled = false;
        intro1Text.enabled = false;

        intro2Image.color = new Color(1, 1, 1, 0);
        intro2Image.enabled = false;
        intro2Text.enabled = false;

        intro3Image.color = new Color(1, 1, 1, 0);
        intro3Image.enabled = false;
        intro3Text.enabled = false;

        intro4Image.color = new Color(1, 1, 1, 0);
        intro4Image.enabled = false;
        intro4Text.enabled = false;
    }

    public IEnumerator ShowCutscene()
    {
        // IMAGE 1
        intro1Image.enabled = true;
        yield return ImageFadeTo(1.0f, 1, intro1Image);
        intro1Text.enabled = true;
        yield return new WaitForSeconds(10);
        intro1Text.enabled = false;
        yield return ImageFadeTo(0.0f, 1, intro1Image);
        intro1Image.enabled = false;

        // IMAGE 2
        intro2Image.enabled = true;
        yield return ImageFadeTo(1.0f, 1, intro2Image);
        intro2Text.enabled = true;
        yield return new WaitForSeconds(7);
        intro2Text.enabled = false;
        yield return ImageFadeTo(0.0f, 1, intro2Image);
        intro2Image.enabled = false;

        // IMAGE 3
        intro3Image.enabled = true;
        yield return ImageFadeTo(1.0f, 1, intro3Image);
        intro3Text.enabled = true;
        yield return new WaitForSeconds(5);
        intro3Text.enabled = false;
        yield return ImageFadeTo(0.0f, 1, intro3Image);
        intro3Image.enabled = false;

        // IMAGE 4
        intro4Image.enabled = true;
        yield return ImageFadeTo(1.0f, 1, intro4Image);
        intro4Text.enabled = true;
        yield return new WaitForSeconds(15);
        intro4Text.enabled = false;
        yield return ImageFadeTo(0.0f, 1, intro4Image);
        intro4Image.enabled = false;
    }
}
