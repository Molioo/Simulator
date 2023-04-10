using UnityEngine;

public class GlobalBoolVariableRequirement : Requirement
{
    [SerializeField]
    private GlobalBoolVariable _globalBoolVariable = null;

    [SerializeField]
    private bool _requiredValue = false;

    public override bool IsRequirementMet()
    {
        return _globalBoolVariable.Value == _requiredValue;
    }

   
}
