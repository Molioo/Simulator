using System.Collections;
using UnityEngine;

public class PreciseActionHammer : PreciseActionInputReceiver
{
    private bool _isHitting = false;
    private Vector3 _randomOffset;

    private int _randomXOffset;
    private int _randomZOffset;

    private PreciseActionNail _currentNail;

    private void Awake()
    {
        _randomXOffset = Random.Range(1, 30);
        _randomZOffset = Random.Range(1, 30);
    }

    private void Update()
    {
        CalculateOffset();
    }

    private void CalculateOffset()
    {
        _randomOffset.x = Mathf.Sin((_randomXOffset + Time.time) * 3f) * 0.1f;
        _randomOffset.z = Mathf.Sin((_randomZOffset + Time.time) * 3f) * 0.1f;
    }

    public override void HandleMove(Vector3 moveInput)
    {
        if (_isHitting)
            return;

        transform.Translate((moveInput + _randomOffset) * Time.deltaTime, FPSCameraController.Instance.CameraPositionTransform);

        transform.position += (moveInput + _randomOffset) * Time.deltaTime;
    }

    public override void OnActionKeyDown()
    {
        if (!_isHitting)
            StartCoroutine(TryHitRoutine());
    }

    public override void OnActionKeyUp()
    {
    }

    private IEnumerator TryHitRoutine()
    {
        _isHitting = true;
        yield return new WaitForSeconds(0.5f);
        if (_currentNail != null)
        {
            _currentNail.HitWithHammer();
        }

        _isHitting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PreciseActionNail nail))
        {
            Debug.Log("Enter");
            _currentNail = nail;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PreciseActionNail nail))
        {
            Debug.Log("Exit");
            if (_currentNail == nail)
                _currentNail = null;
        }
    }
}
