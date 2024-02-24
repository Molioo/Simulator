using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivatedWithProgress : MonoBehaviour, ISaveable
{
    [SerializeField]
    private string _uniqueId;

    [SerializeField]
    private int _disableOnStep = 1;

    private int _currentStepIndex = 0;

    public string UniqueID { get => _uniqueId; }

    public void IncreaseProgressStep()
    {
        _currentStepIndex++;
        
        if (_currentStepIndex >= _disableOnStep)
        {
            gameObject.SetActive(false);
        }
    }

    public Dictionary<string, string> OnSave()
    {
        Dictionary<string, string> dataToSave = new Dictionary<string, string>
        {
            {"csi", _currentStepIndex.ToString() }
        };
        return dataToSave;
    }

    public void OnLoad(Dictionary<string, string> data)
    {
        if (data.ContainsKey("csi"))
        {
            _currentStepIndex = int.Parse(data["csi"]);

            if (_currentStepIndex >= _disableOnStep)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
