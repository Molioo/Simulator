using Molioo.Simulator.Items;
using System.Collections.Generic;
using UnityEngine;
using static SaveSystem;


public class PlayerInventoryManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private Item _defaultItemPrefab;

    [SerializeField]
    private Animator _playerAnimator = null;

    [SerializeField]
    private List<Item> _playerCurrentItems = new List<Item>();

    [SerializeField]
    private Transform _itemsTransform = null;

    private Item _currentItem = null;

    public Item CurrentItem { get { return _currentItem; } }

    public List<Item> AllItems {  get { return _playerCurrentItems; } }

    public string UniqueID => "inventory";

    private void Update()
    {
        if (GameManager.Instance.CurrentPlayerGameMode != EPlayerGameMode.PlayerMovement)
        {
            return;

        }

        CheckForItemSelectInput();
        UpdateCurrentItem();
    }

    public void AddItem(ItemTemplate itemTemplate, int amountToAdd = 1)
    {
        if (itemTemplate.Stackable)
        {
            TryToAddStackableItem(itemTemplate, amountToAdd);
        }
        else
        {
            TryToAddNormalItem(itemTemplate);
        }
    }

    private void TryToAddNormalItem(ItemTemplate template)
    {
        if (HasItem(template) == false)
        {
            Item normalItem = SpawnItem(template);
            normalItem.Amount = 1;
            normalItem.gameObject.SetActive(false);
            _playerCurrentItems.Add(normalItem);

            if (template.AlwaysEquipped)
            {
                SetCurrentItem(normalItem);
            }
        }
    }

    private void TryToAddStackableItem(ItemTemplate template, int amountToAdd)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template.ItemId == template.ItemId)
            {
                _playerCurrentItems[i].AddAmount(amountToAdd);

                return;
            }
        }

        //Didn't find any in currentItems
        Item stackableItem = SpawnItem(template);
        stackableItem.Amount = amountToAdd;
        _playerCurrentItems.Add(stackableItem);

        if (template.AlwaysEquipped)
        {
            SetCurrentItem(stackableItem);
        }
    }

    private Item SpawnItem(ItemTemplate itemTemplate)
    {
        if (itemTemplate.ShouldBeSpawnedAsBoneChild)
        {
            Transform boneTransform = _playerAnimator.GetBoneTransform(itemTemplate.BoneParent);
            Item item = Instantiate(itemTemplate.ItemPrefab != null ? itemTemplate.ItemPrefab : _defaultItemPrefab, boneTransform);
            item.Template = itemTemplate;
            item.transform.SetLocalPositionAndRotation(itemTemplate.LocalPositionInBone, Quaternion.Euler(itemTemplate.LocalRotationInBone));
            item.gameObject.name = itemTemplate.Name;
            return item;
        }
        else
        {
            Item item = Instantiate(itemTemplate.ItemPrefab != null ? itemTemplate.ItemPrefab : _defaultItemPrefab, _itemsTransform);
            item.Template = itemTemplate;
            item.gameObject.name = itemTemplate.Name;
            return item;
        }
    }

    public void RemoveItem(ItemTemplate itemTemplate, int amountToRemove)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template.ItemId == itemTemplate.ItemId)
            {
                _playerCurrentItems[i].RemoveAmount(amountToRemove);
                if (_playerCurrentItems[i].Amount <= 0)
                {
                    Destroy(_playerCurrentItems[i].gameObject);
                    _playerCurrentItems.Remove(_playerCurrentItems[i]);
                }
                return;
            }
        }
    }

    public bool HasItem(ItemTemplate template)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template.ItemId == template.ItemId)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasGivenAmountOfItem(ItemTemplate template, int amount)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template.ItemId == template.ItemId)
            {
                if (_playerCurrentItems[i].Amount >= amount)
                {
                    return true;
                }
            }
        }

        return false;

    }

    public bool CanUseItem(ItemTemplate itemTemplate)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template.ItemId == itemTemplate.ItemId)
            {
                return _playerCurrentItems[i].CanBeUsed();
            }
        }
        return false;
    }

    public bool IsItemEquipped(ItemTemplate itemTemplate)
    {
        if (_currentItem == null)
            return false;

        return _currentItem.Template.ItemId == itemTemplate.ItemId;
    }

    private void UpdateCurrentItem()
    {
        if (_currentItem != null)
        {
            _currentItem.UpdateItemBehaviour();
        }
    }

    public void SetCurrentItem(Item item)
    {
        if (_currentItem != null)
        {
            if (_currentItem == item)
                return;

            if (_currentItem.Template.AlwaysEquipped)
                return;

            _currentItem.OnUnequipItem();
        }

        _currentItem = item;
        _currentItem.OnEquipItem();
    }

    private void CheckForItemSelectInput()
    {
        //This is shitty btw, only for tests
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_playerCurrentItems.Count > 0)
            {
                SetCurrentItem(_playerCurrentItems[0]);
            }
        }
    }

    public Dictionary<string, string> OnSave()
    {
        string content = JsonUtility.ToJson(new InventoryWrapper(_playerCurrentItems));
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                { "items", content }
            };
        return keyValuePairs;
    }

    public void OnLoad(Dictionary<string, string> data)
    {
        if (data.ContainsKey("items"))
        {
            InventoryWrapper wrapper = JsonUtility.FromJson<InventoryWrapper>(data["items"]);
            if (wrapper.Items != null)
            {
                foreach (ItemSaveHelper itemSaveHelper in wrapper.Items)
                {
                    AddItem(itemSaveHelper.Template, itemSaveHelper.Amount);
                }
            }
        }
    }
}

public class InventoryWrapper
{
    public List<ItemSaveHelper> Items;

    public InventoryWrapper(List<Item> items)
    {
        Items = new List<ItemSaveHelper>();
        for (int i = 0; i < items.Count; i++)
        {
            Items.Add(new ItemSaveHelper(items[i]));
        }
    }   
}

[System.Serializable]
public class ItemSaveHelper
{
    public ItemTemplate Template;
    public int Amount;

    public ItemSaveHelper(Item item)
    {
        Template = item.Template;
        Amount = item.Amount;
    }
}
