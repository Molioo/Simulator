using Molioo.Simulator.Items;
using System.Collections.Generic;
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

    public void RemoveItem(ItemTemplate itemTemplate)
    {
        for (int i = 0; i < _playerCurrentItems.Count; i++)
        {
            if (_playerCurrentItems[i].Template.ItemId == itemTemplate.ItemId)
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

            _currentItem.OnUnequipItem();
        }

        _currentItem = item;
        _currentItem.OnEquipItem();
    }

    private void CheckForItemSelectInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_playerCurrentItems.Count > 0)
            {
                SetCurrentItem(_playerCurrentItems[0]);
            }
        }
    }

}
