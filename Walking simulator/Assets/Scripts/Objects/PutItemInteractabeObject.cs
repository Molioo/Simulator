using Molioo.Simulator.Items;
using System.Collections.Generic;
using UnityEngine;

public class PutItemInteractabeObject : InteractableObject, ISaveable
{
    [SerializeField]
    private string _uniqueID;

    [SerializeField]
    private ItemTemplate _itemToPlace;

    [SerializeField]
    private int _amountToRequire = 1;

    [SerializeField]
    private Transform _itemNotPutTransform;

    [SerializeField]
    private Transform _itemPutTransform;

    private bool _itemPut = false;

    public string UniqueID => _uniqueID;

    public void PutItem()
    {
        if (PlayerSystemsManager.Instance.InventoryManager.HasGivenAmountOfItem(_itemToPlace, _amountToRequire))
        {
            PlayerSystemsManager.Instance.InventoryManager.RemoveItem(_itemToPlace, _amountToRequire);
            _itemPut = true;
            SetCorrectTransformActive();
        }
    }

    public override bool IsAnyOptionUsable()
    {
        if (_itemPut)
        {
            return false;
        }

        return base.IsAnyOptionUsable();
    }

    private void SetCorrectTransformActive()
    {
        _itemPutTransform.gameObject.SetActive(_itemPut);
        _itemNotPutTransform.gameObject.SetActive(_itemPut == false);
    }

    public Dictionary<string, string> OnSave()
    {
        Dictionary<string, string> dataToSave = new Dictionary<string, string>
        {
            {"ip", _itemPut.ToString() }
        };
        return dataToSave;
    }

    public void OnLoad(Dictionary<string, string> data)
    {
        if (data.ContainsKey("ip"))
        {
            _itemPut = bool.Parse(data["ip"]);
            SetCorrectTransformActive();
        }
    }
}
