using UnityEngine;

public class MovableObject : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _objectRigidbody=null;


    [Header("Visual")]
    [SerializeField]
    private Renderer _objectRenderer;

    [SerializeField]
    private Material _canBePlacedMaterial = null;

    [SerializeField]
    private Material _cantBePlacedMaterial = null;


    public void SetMaterial(bool canBePlaced)
    {
        _objectRenderer.material = canBePlaced ? _canBePlacedMaterial : _cantBePlacedMaterial;
    }

    public void StartedMoving()
    {
        _objectRigidbody.isKinematic = true;
    }

    public void StoppedMoving()
    {
        _objectRigidbody.isKinematic = false;
    }
}
