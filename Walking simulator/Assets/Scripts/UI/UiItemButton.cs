using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private RectTransform rectTransform;

    private Action<Item> onHoverAction;
    private Action<Item> onUnhoverAction;

    private Action<Item> onSelectedAction;

    private Item representedItem;

    public void Setup(Item item, Action<Item> onHover, Action<Item> onUnhover, Action<Item> onSelect)
    {
        representedItem = item;

        itemIcon.sprite = item.Template.Icon;
        onHoverAction = onHover;
        onUnhoverAction = onUnhover;
        onSelectedAction = onSelect;
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.position = position;
    }
   
    public void OnItemClicked()
    {
        onSelectedAction?.Invoke(representedItem);
    }

    public void OnItemHovered()
    {
        onHoverAction?.Invoke(representedItem);
    }

    public void OnItemUnhovered()
    {
        onUnhoverAction?.Invoke(representedItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnItemHovered();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnItemUnhovered();
    }
}
