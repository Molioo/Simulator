using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetupObject : MonoBehaviour
{
    [SerializeField]
    private bool _waitForSaveSystemLoad = false;

    [SerializeField]
    private List<RequirementsActionPair> _setupSteps = new List<RequirementsActionPair>();
   
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
                _setupSteps[i].Action?.Invoke();
            }
        }
    }

}
