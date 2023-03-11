using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToSpawn = null;

    public void SpawnObject()
    {
        if (_objectToSpawn == null)
            return;

        Instantiate(_objectToSpawn, transform.position, transform.rotation, null);
    }
   
}
