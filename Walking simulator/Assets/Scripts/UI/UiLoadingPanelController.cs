using System.Collections;
using UnityEngine;

public class UiLoadingPanelController : Singleton<UiLoadingPanelController>
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    private float _fadeTime = 0.5f;

    public IEnumerator FadeIn()
    {
        yield return FadeLoading(true);
    }

    public IEnumerator FadeOut()
    {
        yield return FadeLoading(false);
    }

    private IEnumerator FadeLoading(bool visible)
    {
        _canvasGroup.interactable = visible;
        _canvasGroup.blocksRaycasts = visible;

        float time = 0;

        while (time < _fadeTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(visible ? 0f : 1f, visible ? 1f : 0f, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = visible ? 1f : 0f;
        yield break;
    }
}
