using Molioo.Simulator.Items;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerInventoryManager : MonoBehaviour
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

    private void Update()
    {
        if (GameManager.Instance.CurrentPlayerGameMode != EPlayerGameMode.PlayerMovement)
            return;

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
        if (!HasItem(template))
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

    private void SetCurrentItem(Item item)
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

}
