using System;
using System.Collections;
using UnityEngine;

public class FadePanel : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private float fadeTime = 0.3f;
    

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public IEnumerator FadeOut()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        yield return Fade(0f, 1f, fadeTime);
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(1f, 0f, fadeTime);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        canvasGroup.alpha = startAlpha;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            float t = time / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}

