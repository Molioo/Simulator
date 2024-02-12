using Molioo.Simulator.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ItemChooserPanelController : UiWindowBase
{
    [SerializeField]
    private UiItemButton itemButtonPrefab;

    [SerializeField]
    private RectTransform itemsRect;

    [SerializeField]
    private RectTransform centerRect;

    [SerializeField]
    private float distanceFromCenter = 400f;

    [SerializeField]
    private CanvasGroup itemChooserCanvasGroup;

    private List<UiItemButton> itemButtons = new List<UiItemButton>();

    private Action<Item> currentOnItemChosen;

    private bool isOpened = false;

    private Item currentlyHoveredItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isOpened == false)
        {
            ShowForItems(PlayerSystemsManager.Instance.InventoryManager.AllItems, (i) => Debug.Log(i.Template.Name));
        }

        if(Input.GetKeyUp(KeyCode.Q) && isOpened)
        {
            if(currentlyHoveredItem!= null)
            {
                ItemSelected(currentlyHoveredItem);
            }
            else
            {
                Hide();
            }
        }
    }

    public void ShowForItems(List<Item> items, Action<Item> onItemChosen)
    {
        if (items == null || items.Count == 0)
            return;

        base.Show();
        
        isOpened = true;
        currentlyHoveredItem = null;
        currentOnItemChosen = onItemChosen;

        GameManager.Instance.SwitchCursorVisible(true);

        SetCanvasGroup(true);
        SpawnItemButtons(items, onItemChosen);
        RepositionItemButtons();
    }

    private void SpawnItemButtons(List<Item> items, Action<Item> onItemChosen)
    {
        foreach (Item item in items)
        {
            UiItemButton itemButton = Instantiate<UiItemButton>(itemButtonPrefab, itemsRect);
            itemButton.Setup(item, ItemHovered, ItemUnhovered, ItemSelected);
            itemButtons.Add(itemButton);
        }
    }

    private void RepositionItemButtons()
    {
        float intervalDegrees = 360f / itemButtons.Count;
        Vector2 center = centerRect.position;

        float degrees = 90;
       
        for (int i = 0; i < itemButtons.Count; i++)
        {
            float radians = degrees * Mathf.Deg2Rad;
            Vector2 vec2 = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            Vector2 targetPosition = center + vec2 * distanceFromCenter;
            itemButtons[i].SetPosition(targetPosition);
            degrees += intervalDegrees;
        }
    }

    public override void Hide()
    {
        base.Hide();

        isOpened = false;
        SetCanvasGroup(false);
        DestroyAllItems();

        GameManager.Instance.SwitchCursorVisible(false);
    }

    private void DestroyAllItems()
    {
        foreach (UiItemButton item in itemButtons)
        {
            Destroy(item.gameObject);
        }
        itemButtons.Clear();
    }

    public void ItemHovered(Item item)
    {
        Debug.Log("Item hovered " + item.Template.Name);
        currentlyHoveredItem = item;
    }

    public void ItemUnhovered(Item item)
    {
        Debug.Log("Item hovered " + item.Template.Name);
        if(currentlyHoveredItem == item)
        {
            currentlyHoveredItem = null;
        }
    }

    public void ItemSelected(Item item)
    {
        Debug.Log("Item selected " + item.Template.Name);
        currentOnItemChosen?.Invoke(item);

        Hide();
    }

    private void SetCanvasGroup(bool isOpened)
    {
        itemChooserCanvasGroup.interactable = isOpened;
        itemChooserCanvasGroup.blocksRaycasts = isOpened;
        itemChooserCanvasGroup.alpha = isOpened ? 1: 0;
    }
}
