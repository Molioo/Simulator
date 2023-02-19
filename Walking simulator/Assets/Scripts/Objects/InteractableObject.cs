using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableObject : MonoBehaviour
{
    public float InteractionDistance = 10;

    public List<InteractionOption> Interactions
    {
        get { return _interactions; }
    }

    [SerializeField]
    private List<InteractionOption> _interactions = new List<InteractionOption>();

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

    public bool IsAnyOptionUsable()
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
        for (int i = 0; i < _interactions.Count; i++)
        {
            if (_interactions[i].CanBeUsed())
            {
                builder.Append("[" + _interactions[i].InteractionKeyCode.ToString() + "] " + _interactions[i].InteractionText + Environment.NewLine);
            }
        }
        return builder.ToString();
    }

    private void OnValidate()
    {
        for (int i = 0; i < _interactions.Count; i++)
        {
            _interactions[i].OnValidate();
        }
    }

}
