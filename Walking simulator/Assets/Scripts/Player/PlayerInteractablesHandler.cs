using UnityEngine;

public class PlayerInteractablesHandler : MonoBehaviour
{
    [SerializeField]
    private FPSCameraController _cameraController;

    [SerializeField]
    private LayerMask _objectsRaycastLayerMask;

    private InteractableObject _currentlyFocusedInteractable = null;

    private void Update()
    {
        RaycastForInteractableObjects();
        HandleInteractInput();
    }

    private void RaycastForInteractableObjects()
    {
            _currentlyFocusedInteractable = null;

        RaycastHit hit;
        if (Physics.Raycast(_cameraController.transform.position,_cameraController.transform.forward,out hit,100))
        {
            if(hit.collider.gameObject.TryGetComponent(out InteractableObject interactable))
            {
                _currentlyFocusedInteractable = interactable;
            }
        }
    }

    private void HandleInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_currentlyFocusedInteractable!=null)
            {
                _currentlyFocusedInteractable.Interact(this);
            }
        }
    }
}
