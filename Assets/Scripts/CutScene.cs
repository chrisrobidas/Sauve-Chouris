using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    protected IEnumerator ImageFadeTo(float value, float time, Image imageToModify)
    {
        float alpha = imageToModify.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, value, t));
            imageToModify.color = newColor;
            yield return null;
        }
    }
}
