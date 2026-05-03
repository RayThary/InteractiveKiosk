using System;
using System.Collections;
using UnityEngine;

public class FadePanel : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public IEnumerator FadeOut(float _fadeTime)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        yield return Fade(0f, 1f, _fadeTime);
    }

    public IEnumerator FadeIn(float _fadeTime)
    {
        yield return Fade(1f, 0f, _fadeTime);

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

