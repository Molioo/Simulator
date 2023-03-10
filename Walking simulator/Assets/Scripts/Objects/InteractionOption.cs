using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractionOption 
{
    public string InteractionText;

    public KeyCode InteractionKeyCode = KeyCode.E;

    public UnityEvent OnPlayerInteract = null;

    public List<Requirement> Requirements = new List<Requirement>();

    public bool CanBeUsedMoreThanOnce = true;

    public bool DestroyAfterInteraction = false;

    private bool _wasUsedAlready = false;

    public bool CanBeUsed()
    {
        if (!HasMetAllRequirements())
            return false;

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
            _wasUsedAlready = true;
        }
    }

    public void OnValidate()
    {
        if (InteractionKeyCode == KeyCode.None)
            InteractionKeyCode = KeyCode.E;
    }
  
    private bool HasMetAllRequirements()
    {
        for (int i = 0; i < Requirements.Count; i++)
        {
            if (!Requirements[i].IsRequirementMet())
                return false;
        }

        return true;
    }
}
