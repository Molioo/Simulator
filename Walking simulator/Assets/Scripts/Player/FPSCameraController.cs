using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FPSCameraController : Singleton<FPSCameraController>
{
    [SerializeField] private Transform _playerTransform = null;
    [SerializeField] private Transform _cameraPositionTransform = null;

    [SerializeField] private float _cameraSensitivity = 3;
    [SerializeField] private float _maxUpAngle = 80;
    [SerializeField] private float _maxDownAngle = -80;

    private float _mouseX;
    private float _mouseY;

    private float _rotX = 0.0f;
    private float _rotY = 0.0f;
    private float _rotZ = 0.0f;

    protected override void Awake()
    {
        base.Awake();
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Mouse input
        _mouseX = Input.GetAxis("Mouse X") * _cameraSensitivity;
        _mouseY = Input.GetAxis("Mouse Y") * _cameraSensitivity;

        // Calculations
        _rotX -= _mouseY;
        _rotX = Mathf.Clamp(_rotX, _maxDownAngle, _maxUpAngle);
        _rotY += _mouseX;

        // Placing values
        transform.localRotation = Quaternion.Euler(_rotX, _rotY, _rotZ);
        _playerTransform.Rotate(Vector3.up * _mouseX);
        transform.position = _cameraPositionTransform.position;
    }

    public void Shake(float magnitude, float duration)
    {
        StartCoroutine(IShake(magnitude, duration));
    }

    private IEnumerator IShake(float mag, float dur)
    {
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        for (float t = 0.0f; t <= dur; t += Time.deltaTime)
        {
            _rotZ = Random.Range(-mag, mag) * (t / dur - 1.0f);
            yield return wfeof;
        }
        _rotZ = 0.0f;
    }
}
