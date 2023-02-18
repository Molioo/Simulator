using Molioo.Simulator.Items;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _playerCurrentItems = new List<Item>();

    [SerializeField]
    private Transform _itemsTransform = null;

    private Item _currentItem = null;

    public void AddItem(ItemTemplate itemTemplate)
    {
        if (itemTemplate.UsesAdditionalPrefab)
        {
            Item item = Instantiate(itemTemplate.ItemPrefab, _itemsTransform);
            _playerCurrentItems.Add(item);
        }
        else
        {
            Item item = new Item(itemTemplate);
            _playerCurrentItems.Add(item);
        }
    }

    public void RemoveItem(ItemTemplate itemTemplate)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template == itemTemplate)
            {
                _playerCurrentItems.Remove(_playerCurrentItems[i]);
                return;
            }
        }
    }

    private void Update()
    {
        CheckForItemSelectInput();
        UpdateCurrentItem();
    }

    private void UpdateCurrentItem()
    {
        if(_currentItem!=null)
        {
            _currentItem.UpdateItemBehaviour();
        }
    }

    private void SetCurrentItem(Item item)
    {
        if (_currentItem != null)
        {
            if (_currentItem == item)
                return;

            _currentItem.OnUnequipItem();
        }

        _currentItem = item;
        _currentItem.OnEquipItem();
    }

    private void CheckForItemSelectInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_playerCurrentItems.Count>0)
            {
                SetCurrentItem(_playerCurrentItems[0]);
            }
        }
    }

}
