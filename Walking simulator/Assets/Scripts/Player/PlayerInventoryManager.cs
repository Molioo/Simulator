using Molioo.Simulator.Items;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _playerCurrentItems = new List<Item>(); 

    public void AddItem(Item item)
    {
        if (!_playerCurrentItems.Contains(item))
        {
            _playerCurrentItems.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (_playerCurrentItems.Contains(item))
        {
            _playerCurrentItems.Remove(item);
        }
    }
}
