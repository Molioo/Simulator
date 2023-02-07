using UnityEngine;

public class PlayerInteractablesHandler : MonoBehaviour
{
    public static InteractableObject CurrentlyFocusedInteractable = null;

    [SerializeField]
    private FPSCameraController _cameraController;

    [SerializeField]
    private LayerMask _objectsRaycastLayerMask;

    private float _distanceToCurrentObject;

    private void Update()
    {
        RaycastForInteractableObjects();
        HandleInteractInput();
    }

    private void RaycastForInteractableObjects()
    {
        CurrentlyFocusedInteractable = null;

        RaycastHit hit;
        if (Physics.Raycast(_cameraController.transform.position, _cameraController.transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.TryGetComponent(out InteractableObject interactable))
            {
                if (!interactable.CanBeUsed())
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CurrentlyFocusedInteractable != null && CurrentlyFocusedInteractable.CanBeUsed())
            {
                CurrentlyFocusedInteractable.Interact(this);
            }
        }
    }
}
