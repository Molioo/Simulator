using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Template", menuName = "Molioo/Variables/New Global Bool Variable")]
public class GlobalBoolVariable : ScriptableObject, ISaveable
{
    [SerializeField]
    private string _uniqueID = "";

    public bool Value;

    public bool InitialValue;   

    private const string UID_PREFIX = "gbv_";

    public string UniqueID => UID_PREFIX + _uniqueID;

    public Dictionary<string, string> OnSave()
    {
        Dictionary<string, string> dataToSave = new Dictionary<string, string>
        {
            {nameof(Value), Value.ToString() }
        };
        return dataToSave;
    }

    public void OnLoad(Dictionary<string, string> data)
    {
        if (data.ContainsKey(nameof(Value)))
        {
            Value = bool.Parse(data[nameof(Value)]);
        }
    }
}
