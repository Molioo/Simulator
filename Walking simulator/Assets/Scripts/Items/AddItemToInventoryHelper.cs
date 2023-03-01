using UnityEngine;

namespace Molioo.Simulator.Items
{
    public class AddItemToInventoryHelper : MonoBehaviour
    {
        [SerializeField]
        private ItemTemplate _itemToAdd = null;

        [SerializeField]
        private int _amountToAdd = 1;

        public void AddItemToInventory()
        {
            if (_itemToAdd != null)
                PlayerSystemsManager.Instance.InventoryManager.AddItem(_itemToAdd, _amountToAdd);
        }

    }
}