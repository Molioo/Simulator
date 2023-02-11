using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public string InteractionText;

    public float InteractionDistance = 10;

    [SerializeField]
    private UnityEvent _onPlayerInteract = null;

    [SerializeField]
    private bool _canBeUsedMoreThanOnce = false;

    [SerializeField]
    private bool _destroyAfterInteraction = false;

    private bool _wasUsedAlready = false;

    public void Interact(PlayerInteractablesHandler playerInteractablesHandler)
    {
        if (_onPlayerInteract != null)
            _onPlayerInteract.Invoke();

        _wasUsedAlready = true;

        Debug.Log("Player interacted with object " + gameObject.name);

        if(_destroyAfterInteraction)
        {
            Destroy(gameObject);
        }
    }

    public bool CanBeUsed()
    {
        if (_canBeUsedMoreThanOnce)
            return true;
        else
            return !_wasUsedAlready;
    }
}
