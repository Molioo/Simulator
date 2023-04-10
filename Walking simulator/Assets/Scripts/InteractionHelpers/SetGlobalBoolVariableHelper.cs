using UnityEngine;

public class SetGlobalBoolVariableHelper : MonoBehaviour
{
    [SerializeField]
    private GlobalBoolVariable _globaBoolVariable = null;


    public void SetValue(bool value)
    {
        _globaBoolVariable.Value = value;
    }
}
