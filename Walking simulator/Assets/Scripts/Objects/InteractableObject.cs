using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ObjectName;

    public float InteractionDistance = 10;

    public List<InteractionOption> Interactions
    {
        get { return _interactions; }
    }

    [SerializeField]
    private List<InteractionOption> _interactions = new List<InteractionOption>();

    private int _interactableKeyCounter = 0;

    public void Interact(InteractionOption interaction)
    {
        if(interaction.CanBeUsed())
        {
            interaction.Use();
            if(interaction.DestroyAfterInteraction)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual bool IsAnyOptionUsable()
    {
        for (int i = 0; i < _interactions.Count; i++)
        {
            if (_interactions[i].CanBeUsed())
                return true;
        }
        return false;
    }

    public string GetInteractionsText()
    {
        StringBuilder builder = new StringBuilder();
        if (string.IsNullOrEmpty(ObjectName) == false)
        {
            builder.Append(ObjectName + Environment.NewLine);
        }

        _interactableKeyCounter = 0;
        for (int i = 0; i < _interactions.Count; i++)
        {
            if (_interactions[i].CanBeUsed())
            {
                builder.Append("[" + PlayerInteractablesHandler.InteractionKeyCodes[_interactableKeyCounter].ToString() + "] " + _interactions[i].InteractionText + Environment.NewLine);
                _interactableKeyCounter++;
            }
        }
        return builder.ToString();
    }
}
