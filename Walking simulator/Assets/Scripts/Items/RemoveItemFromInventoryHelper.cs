using Molioo.Simulator.Items;
using UnityEngine;

public class RemoveItemFromInventoryHelper : MonoBehaviour
{
    [SerializeField]
    private ItemTemplate _itemToAdd = null;

    [SerializeField]
    private int _amountToRemove = 1;


    public void RemoveItemFromInventory()
    {
        if (_itemToAdd != null)
            PlayerSystemsManager.Instance.InventoryManager.RemoveItem(_itemToAdd, _amountToRemove);
    }
}
