using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Other components")]
    [SerializeField] private Animator _playerAnimator=null;

    [Header("Movement properties")]
    [SerializeField] private float _walkSpeed = 8f;
    [SerializeField] private float _runSpeed = 12f;
    [SerializeField] private float _maximumPlayerSpeed = 150.0f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 500.0f;
    [SerializeField] private float _jumpCooldown = 1.0f;
    private bool _jumpBlocked = false;

    [Header("GroundCheck")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckerDist = 0.2f;

    private Rigidbody _playerRigidbody;

    private float _verticalInput;
    private float _horizontalInput;
    private float _previousYVelocity;

    private Vector3 _inputForce;
    private bool _isGrounded = true;

    private void Awake()
    {
        InitializeComponents();
    }

    private void FixedUpdate()
    {
        SetIsGrounded();
        SetInputValues();
        HandleMovement();
        SetAnimatorValues();
    }

    private void InitializeComponents()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void SetIsGrounded()
    {
        _isGrounded = (Mathf.Abs(_playerRigidbody.velocity.y - _previousYVelocity) < .1f) && (Physics.OverlapSphere(_groundChecker.position, _groundCheckerDist).Length > 1); // > 1 because it also counts the player
        _previousYVelocity = _playerRigidbody.velocity.y;
    }

    private void SetInputValues()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void SetAnimatorValues()
    {
        _playerAnimator.SetFloat("Horizontal", _horizontalInput);
        _playerAnimator.SetFloat("Vertical", _verticalInput);
    }

    private void HandleMovement()
    {
        _playerRigidbody.velocity = ClampMag(_playerRigidbody.velocity, _maximumPlayerSpeed);

        _inputForce = (transform.forward * _verticalInput + transform.right * _horizontalInput).normalized * (Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed);

        if (_isGrounded)
        {
            // Jump
            if (Input.GetButton("Jump") && !_jumpBlocked)
            {
                _playerRigidbody.AddForce(-_jumpForce * _playerRigidbody.mass * Vector3.down);
                _jumpBlocked = true;
                Invoke(nameof(UnblockJump), _jumpCooldown);
            }
            // Ground controller
            _playerRigidbody.velocity = Vector3.Lerp(_playerRigidbody.velocity, _inputForce, 10 * Time.fixedDeltaTime);
        }
        else
        {
            _playerRigidbody.velocity = ClampSqrMag(_playerRigidbody.velocity + _inputForce * Time.fixedDeltaTime, _playerRigidbody.velocity.sqrMagnitude);
        }
    }

    private void UnblockJump()
    {
        _jumpBlocked = false;
    }

    private static Vector3 ClampSqrMag(Vector3 vec, float sqrMag)
    {
        if (vec.sqrMagnitude > sqrMag)
            vec = vec.normalized * Mathf.Sqrt(sqrMag);
        return vec;
    }

    private static Vector3 ClampMag(Vector3 vector, float maxMag)
    {
        if (vector.sqrMagnitude > maxMag * maxMag)
            vector = vector.normalized * maxMag;
        return vector;
    }


}
