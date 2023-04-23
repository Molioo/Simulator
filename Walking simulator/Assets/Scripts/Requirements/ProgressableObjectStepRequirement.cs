using UnityEngine;

public class ProgressableObjectStepRequirement : Requirement
{
    [SerializeField]
    private ProgressableObject _progressableObject = null;

    [SerializeField]
    private int _requiredProgressStep = 0;

    public override bool IsRequirementMet()
    {
        return _progressableObject.CurrentStepIndex == _requiredProgressStep;
    }

    
}
