using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField]
    private RequirementsActionPair _triggerRequirementsActionPair;

    [SerializeField]
    private bool _canBeInvokedMoreThanOnce;

    private bool _invokedAlready;


    private void OnTriggerEnter(Collider other)
    {
        if (!_canBeInvokedMoreThanOnce && _invokedAlready)
            return;

        if (other.gameObject.TryGetComponent(out PlayerMovementController player))
        {
            if(_triggerRequirementsActionPair.HasMetAllRequirements())
            {
                _triggerRequirementsActionPair.Action?.Invoke();
                _invokedAlready = true;
            }
        }
    }
}
