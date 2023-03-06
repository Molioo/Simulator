using System.Collections.Generic;
using UnityEngine;

public class ProgressableObject : MonoBehaviour
{
    [SerializeField]
    private EObjectProgressionMethod _progressionMethod;

    [SerializeField]
    private List<ObjectProgressStep> _objectProgressSteps = new List<ObjectProgressStep>();

    [SerializeField]
    private int _currentStepIndex = 0;

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
                EnableChildObject(_currentStepIndex);
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

    private void EnableChildObject(int progressStep)
    {
        for (int i = 0; i < _objectProgressSteps.Count; i++)
        {
            if (_objectProgressSteps[i].ProgressStepIndex == progressStep)
            {
                _objectProgressSteps[i].StepObject.gameObject.SetActive(true);
            }
        }
    }
}
