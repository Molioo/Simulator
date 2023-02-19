using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractionOption 
{
    public string InteractionText;

    public KeyCode InteractionKeyCode = KeyCode.E;

    public UnityEvent OnPlayerInteract = null;

    public bool CanBeUsedMoreThanOnce = true;

    public bool DestroyAfterInteraction = false;

    private bool _wasUsedAlready = false;

    public bool CanBeUsed()
    {
        if (CanBeUsedMoreThanOnce)
            return true;
        else
            return !_wasUsedAlready;
    }

    public void Use()
    {
        if(OnPlayerInteract!=null)
        {
            OnPlayerInteract.Invoke();
        }
    }

    public void OnValidate()
    {
        if (InteractionKeyCode == KeyCode.None)
            InteractionKeyCode = KeyCode.E;
    }
  
}
