using UnityEngine;

public class PlayerObjectMovingController : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTransform = null;

    private MovableObject _movedObject = null;

    private float _distanceFromCamera = 0f;

    public void StartMovingObject(MovableObject objectToMove)
    {
        _movedObject = objectToMove;
        _movedObject.StartedMoving();
        _distanceFromCamera = Vector3.Distance(objectToMove.transform.position, _cameraTransform.position);
    }


    public void StopMovingObject()
    {
        _movedObject.StoppedMoving();
        _movedObject = null;
    }

    private void Update()
    {
        if (_movedObject != null)
            _movedObject.transform.position = _cameraTransform.position + _cameraTransform.forward * _distanceFromCamera;
    }

}
