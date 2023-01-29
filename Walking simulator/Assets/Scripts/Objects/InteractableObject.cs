using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onPlayerInteract = null;

    public void Interact(PlayerInteractablesHandler playerInteractablesHandler)
    {
        if (_onPlayerInteract != null)
            _onPlayerInteract.Invoke();

        Debug.Log("Player interacted with object " + gameObject.name);
    }
}
