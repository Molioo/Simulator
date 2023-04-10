using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class RequirementsActionPair 
{
    public UnityEvent Action = null;

    public List<Requirement> Requirements = new List<Requirement>();

    public bool HasMetAllRequirements()
    {
        for (int i = 0; i < Requirements.Count; i++)
        {
            if (!Requirements[i].IsRequirementMet())
                return false;
        }

        return true;
    }
}
