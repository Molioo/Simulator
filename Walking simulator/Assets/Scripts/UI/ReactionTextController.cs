using System.Collections;
using TMPro;
using UnityEngine;

public class ReactionTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _reactionText = null;

    [SerializeField]
    private RectTransform _reactionBackgroundRect = null;

    public void SetReactionText(string text, float reactionTime)
    {
        _reactionBackgroundRect.gameObject.SetActive(true);
        _reactionText.text = text;
        _reactionBackgroundRect.sizeDelta = new Vector2(_reactionText.preferredWidth + 30, _reactionBackgroundRect.sizeDelta.y);
        StopAllCoroutines();
        StartCoroutine(HideReactionText(reactionTime));
    }

    private IEnumerator HideReactionText(float reactionTime)
    {
        yield return new WaitForSeconds(reactionTime);
        _reactionBackgroundRect.gameObject.SetActive(false);
    }

}
