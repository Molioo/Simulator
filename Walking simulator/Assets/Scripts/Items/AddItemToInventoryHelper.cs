using UnityEngine;

namespace Molioo.Simulator.Items
{
    public class AddItemToInventoryHelper : MonoBehaviour
    {
        [SerializeField]
        private Item _itemToAdd = null;

        [SerializeField]
        private bool _destroyOnAdd = true;

        public void AddItemToInventory()
        {
            if (_itemToAdd != null)
                PlayerSystemsManager.Instance.InventoryManager.AddItem(_itemToAdd);

            if (_destroyOnAdd)
                Destroy(gameObject);
        }

    }
}