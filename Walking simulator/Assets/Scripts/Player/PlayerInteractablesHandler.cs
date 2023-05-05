using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractablesHandler : MonoBehaviour
{
    public static InteractableObject CurrentlyFocusedInteractable = null;

    public static List<KeyCode> InteractionKeyCodes = new List<KeyCode>() { KeyCode.E, KeyCode.F, KeyCode.G };

    [SerializeField]
    private FPSCameraController _cameraController;

    [SerializeField]
    private LayerMask _objectsRaycastLayerMask;

    private float _distanceToCurrentObject;

    private int _interactableKeyCounter = 0;

    private void Update()
    {
        RaycastForInteractableObjects();
        HandleInteractInput();
    }

    public static string GetInteractionText()
    {
        if (CurrentlyFocusedInteractable!=null)
        {
            return CurrentlyFocusedInteractable.IsAnyOptionUsable() ? CurrentlyFocusedInteractable.GetInteractionsText() : "";
        }
        return "";
    }

    private void RaycastForInteractableObjects()
    {
        CurrentlyFocusedInteractable = null;

        RaycastHit hit;
        if (Physics.Raycast(_cameraController.transform.position, _cameraController.transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.TryGetComponent(out InteractableObject interactable))
            {
                if (!interactable.IsAnyOptionUsable())
                    return;

                _distanceToCurrentObject = Vector3.Distance(transform.position, hit.collider.transform.position);
                if (_distanceToCurrentObject > interactable.InteractionDistance)
                    return;

                CurrentlyFocusedInteractable = interactable;
            }
        }
    }

    private void HandleInteractInput()
    {
        if (CurrentlyFocusedInteractable == null)
            return;

        if (!CurrentlyFocusedInteractable.IsAnyOptionUsable())
            return;

        _interactableKeyCounter = 0;
        for (int i = 0; i < CurrentlyFocusedInteractable.Interactions.Count; i++)
        {
            if (!CurrentlyFocusedInteractable.Interactions[i].CanBeUsed())
                continue;

            if (Input.GetKeyDown(InteractionKeyCodes[_interactableKeyCounter]))
            {
                CurrentlyFocusedInteractable.Interact(CurrentlyFocusedInteractable.Interactions[i]);
                break;
            }

            _interactableKeyCounter++;
        }
    }
}
