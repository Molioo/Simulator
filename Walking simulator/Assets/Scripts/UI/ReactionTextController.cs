using TMPro;
using UnityEngine;

public class ReactionTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _reactionText = null;

    [SerializeField]
    private RectTransform _reactionBackgroundRect = null;

    public void SetReactionText(string text)
    {
        _reactionBackgroundRect.gameObject.SetActive(true);
        _reactionText.text = text;
        _reactionBackgroundRect.sizeDelta = new Vector2(_reactionText.preferredWidth + 30, _reactionBackgroundRect.sizeDelta.y);
        Invoke(nameof(HideReactionText), 3f);
    }

    private void HideReactionText()
    {
        _reactionBackgroundRect.gameObject.SetActive(false);
    }

}
