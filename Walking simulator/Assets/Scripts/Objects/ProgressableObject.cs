using System.Collections.Generic;
using UnityEngine;

public class ProgressableObject : MonoBehaviour, ISaveable
{
    [SerializeField]
    private EObjectProgressionMethod _progressionMethod;

    [SerializeField]
    private List<ObjectProgressStep> _objectProgressSteps = new List<ObjectProgressStep>();

    [SerializeField]
    private int _currentStepIndex = 0;

    [SerializeField]
    private string _uniqueId;

    public string UniqueID { get => _uniqueId; }

    public int CurrentStepIndex { get => _currentStepIndex; }


    private void Awake()
    {
        InitializeObject();
    }

    private void InitializeObject()
    {
        //Load progress step index

        switch (_progressionMethod)
        {
            case EObjectProgressionMethod.UseChildObjects:
                SetChildObjectActive(_currentStepIndex,true);
                break;
            case EObjectProgressionMethod.SpawnPrefabs:
                SpawnObject(_currentStepIndex);
                break;
        }
    }

    private void SpawnObject(int progressStep)
    {
        for (int i = 0; i < _objectProgressSteps.Count; i++)
        {
            if (_objectProgressSteps[i].ProgressStepIndex == progressStep)
            {
                _objectProgressSteps[i].SpawnObject(transform);
            }
        }
    }

    private void SetChildObjectActive(int progressStep, bool active)
    {
        for (int i = 0; i < _objectProgressSteps.Count; i++)
        {
            if (_objectProgressSteps[i].ProgressStepIndex == progressStep)
            {
                _objectProgressSteps[i].StepObject.gameObject.SetActive(active);
            }
        }
    }

    public void IncreaseProgressStep()
    {
        switch (_progressionMethod)
        {
            case EObjectProgressionMethod.UseChildObjects:
                SetChildObjectActive(_currentStepIndex, false);
                _currentStepIndex++;
                SetChildObjectActive(_currentStepIndex, true);
                break;
            case EObjectProgressionMethod.SpawnPrefabs:
                SpawnObject(_currentStepIndex);
                break;
        }
    }

    public void DecreaseProgressStep()
    {
        switch (_progressionMethod)
        {
            case EObjectProgressionMethod.UseChildObjects:
                SetChildObjectActive(_currentStepIndex, false);
                _currentStepIndex--;
                SetChildObjectActive(_currentStepIndex, true);
                break;
            case EObjectProgressionMethod.SpawnPrefabs:
                SpawnObject(_currentStepIndex);
                break;
        }
    }

    public Dictionary<string, object> OnSave()
    {
        Dictionary<string, object> dataToSave = new Dictionary<string, object>
        {
            {nameof(_currentStepIndex), _currentStepIndex }
        };
        return dataToSave;
    }

    public void OnLoad(Dictionary<string, object> data)
    {
        if (data.ContainsKey(nameof(_currentStepIndex)))
        {
            _currentStepIndex = (int)data[nameof(_currentStepIndex)];
        }
    }
}
