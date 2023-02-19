using TMPro;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField]
    private TextMeshProUGUI _interactionText = null;

    [SerializeField]
    private CanvasGroup _canvasGroup = null;

    private void Update()
    {
        SetInteractionText();
    }

    private void SetInteractionText()
    {
        _interactionText.text = PlayerInteractablesHandler.GetInteractionText();
    }

    public void SwitchAllUIVisibility(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
    }

  
}
