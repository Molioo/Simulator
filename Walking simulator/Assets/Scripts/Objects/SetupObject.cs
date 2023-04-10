using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetupObject : MonoBehaviour
{
    [SerializeField]
    private bool _waitForSaveSystemLoad = false;

    [SerializeField]
    private List<SetupStep> _setupSteps = new List<SetupStep>();
   
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < _setupSteps.Count; i++)
        {
            if (_setupSteps[i].HasMetAllRequirements())
            {
                _setupSteps[i].SetupActions?.Invoke();
            }
        }
    }

    [System.Serializable]
    private class SetupStep
    {
        public UnityEvent SetupActions = null;

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

}
