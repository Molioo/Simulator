using UnityEngine;
using UnityEngine.Events;

public class TrashBagItem : Item
{
    public int Capacity = 10;

    [SerializeField]
    private UnityEvent _onCapacityReached = null;

    private int _currentTrashCount = 0;

    public override bool CanBeUsed()
    {
        return _currentTrashCount < Capacity;
    }

    public void TrashCollected()
    {
        _currentTrashCount++;
        if(_currentTrashCount >= Capacity)
        {
            _onCapacityReached?.Invoke();
        }
    }
}
