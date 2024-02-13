using TMPro;
using UnityEngine;

public class UIInteractionTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _interactionText = null;

    [SerializeField]
    private RectTransform _interactionBackgroundRect = null;

    private void Update()
    {
       SetTextAndBackground();
    }

    private void SetTextAndBackground()
    {
        _interactionText.text = PlayerInteractablesHandler.GetInteractionText();

        if (string.IsNullOrEmpty(_interactionText.text))
        {
            _interactionBackgroundRect.sizeDelta = Vector2.zero;
        }
        else
        {
            _interactionBackgroundRect.sizeDelta = new Vector2(_interactionText.preferredWidth + 30, _interactionText.preferredHeight + 30);
        }
    }
}
