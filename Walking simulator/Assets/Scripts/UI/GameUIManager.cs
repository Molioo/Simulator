using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _interactionText = null;

    private void Update()
    {
        SetInteractionText();
    }

    private void SetInteractionText()
    {
        _interactionText.text = PlayerInteractablesHandler.CurrentlyFocusedInteractable!= null ? PlayerInteractablesHandler.CurrentlyFocusedInteractable.InteractionText : "";
    }

  
}
