using Molioo.Simulator.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItemInteractableObject : InteractableObject, ISaveable
{
    [SerializeField]
    private string _uniqueID;

    [SerializeField]
    private ItemTemplate _itemToAdd;

    [SerializeField]
    private int _amountToAdd = 1;

    [SerializeField]
    private bool _disableOnPickUp = true;

    private bool _pickedUp = false;

    public string UniqueID => _uniqueID;

    public void AddItemToInventory()
    {
        if (_itemToAdd != null)
        {
            PlayerSystemsManager.Instance.InventoryManager.AddItem(_itemToAdd, _amountToAdd);
            _pickedUp = true;
            if (_disableOnPickUp)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public Dictionary<string, string> OnSave()
    {
        Dictionary<string, string> dataToSave = new Dictionary<string, string>
        {
            {"pu", _pickedUp.ToString() }
        };
        return dataToSave;
    }

    public void OnLoad(Dictionary<string, string> data)
    {
        if (data.ContainsKey("pu"))
        {
            _pickedUp = bool.Parse(data["pu"]);

            if (_disableOnPickUp && _pickedUp)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
