using UnityEngine;

public class PreciseActionNail : PreciseActionInputReceiver
{
    [SerializeField]
    private Transform _nailTransform = null;

    private int _nailProgress = 0;

    private PreciseActionNailPosition _nailPosition;

    public override void HandleMove(Vector3 moveInput)
    {
        if (_nailProgress > 0)
            return;

        transform.Translate(moveInput * Time.deltaTime, FPSCameraController.Instance.CameraPositionTransform);
    }

    public override void OnActionKeyDown()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnActionKeyUp()
    {
        //throw new System.NotImplementedException();
    }

    public void HitWithHammer()
    {
        if (_nailProgress > 100)
            return;

        _nailProgress += 25;
        _nailTransform.position += Vector3.down * 0.02f;
        if (_nailProgress >= 100)
        {
            _nailProgress = 100;
            if (_nailPosition != null)
                _nailPosition.IsNailed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PreciseActionNailPosition nailPosition))
        {
            Debug.Log("Enter");
            _nailPosition = nailPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PreciseActionNailPosition nailPosition))
        {
            Debug.Log("Exit");
            if (_nailPosition == nailPosition)
                _nailPosition = null;
        }
    }
}
