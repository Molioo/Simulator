using System;
using UnityEngine;

public class PreciseActionController : MonoBehaviour
{
    private const string LEFT_HAND_HORIZONTAL_AXIS_NAME = "Horizontal";
    private const string LEFT_HAND_VERTICAL_AXIS_NAME = "Vertical";

    private const string RIGHT_HAND_HORIZONTAL_AXIS_NAME = "Mouse X";
    private const string RIGHT_HAND_VERTICAL_AXIS_NAME = "Mouse Y";

    private const KeyCode LEFT_HAND_ACTION_KEYCODE = KeyCode.Space;
    private const KeyCode RIGHT_HAND_ACTION_KEYCODE = KeyCode.Mouse0;

   

    [SerializeField]
    private PreciseActionInputReceiver _leftHandInputReceiver;

    [SerializeField]
    private PreciseActionInputReceiver _rightHandInputReceiver;

    private Vector3 _leftHandInput;
    private Vector3 _rightHandInput;

    private void Update()
    {
        HandleDebugInput();

        if (GameManager.Instance.CurrentPlayerGameMode != EPlayerGameMode.PreciseAction)
            return;

        GetHandsInput();
        UpdateInputReceivers();
    }

    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            StartPreciseAction();

        if (Input.GetKeyDown(KeyCode.T))
            FinishPreciseAction();
    }

    public void StartPreciseAction()
    {
        GameManager.Instance.CurrentPlayerGameMode = EPlayerGameMode.PreciseAction;
    }
    
    public void FinishPreciseAction()
    {
        GameManager.Instance.CurrentPlayerGameMode = EPlayerGameMode.PlayerMovement;
    }

    private void GetHandsInput()
    {
        _leftHandInput.x = Input.GetAxis(LEFT_HAND_HORIZONTAL_AXIS_NAME);
        _leftHandInput.z = Input.GetAxis(LEFT_HAND_VERTICAL_AXIS_NAME);

        _rightHandInput.x = Input.GetAxis(RIGHT_HAND_HORIZONTAL_AXIS_NAME);
        _rightHandInput.z = Input.GetAxis(RIGHT_HAND_VERTICAL_AXIS_NAME);
    }

    private void UpdateInputReceivers()
    {
        _leftHandInputReceiver.HandleMove(_leftHandInput);
        if (Input.GetKeyDown(LEFT_HAND_ACTION_KEYCODE))
            _leftHandInputReceiver.OnActionKeyDown();
        if (Input.GetKeyUp(LEFT_HAND_ACTION_KEYCODE))
            _leftHandInputReceiver.OnActionKeyDown();

        _rightHandInputReceiver.HandleMove(_rightHandInput);
        if (Input.GetKeyDown(RIGHT_HAND_ACTION_KEYCODE))
            _rightHandInputReceiver.OnActionKeyDown();
        if (Input.GetKeyUp(RIGHT_HAND_ACTION_KEYCODE))
            _rightHandInputReceiver.OnActionKeyDown();
    }
}
