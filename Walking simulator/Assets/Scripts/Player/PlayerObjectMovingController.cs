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
        if (_movedObject == null)
            return;

        _movedObject.StoppedMoving();
        _movedObject = null;
    }

    private void Update()
    {
        HandleDistanceChange();
        MoveObject();
        CheckMovingInput();
    }

    private void MoveObject()
    {
        if (_movedObject != null)
            _movedObject.transform.position = Vector3.Lerp(_movedObject.transform.position, _cameraTransform.position + _cameraTransform.forward * _distanceFromCamera, Time.deltaTime * 10f);
    }

    private void HandleDistanceChange()
    {
        float mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");
        _distanceFromCamera += mouseScrollValue;
    }

    private void CheckMovingInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopMovingObject();
        }
    }

}
