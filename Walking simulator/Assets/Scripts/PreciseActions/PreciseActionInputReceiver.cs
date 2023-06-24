using UnityEngine;

public abstract class PreciseActionInputReceiver : MonoBehaviour
{
    public abstract void HandleMove(Vector3 moveInput);

    public abstract void OnActionKeyDown();

    public abstract void OnActionKeyUp();
}
