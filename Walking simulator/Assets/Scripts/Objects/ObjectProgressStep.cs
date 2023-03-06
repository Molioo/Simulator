using UnityEngine;

[System.Serializable]
public class ObjectProgressStep
{
    public GameObject StepObject;
    public int ProgressStepIndex;

    private GameObject _spawnedObject;

    public void SpawnObject(Transform parentTransform)
    {
        GameObject spawnedObject = GameObject.Instantiate(StepObject, parentTransform);
        _spawnedObject = spawnedObject;
    }
}


