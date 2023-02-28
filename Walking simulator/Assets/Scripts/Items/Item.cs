using Molioo.Simulator.Items;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Amount = 1;

    public ItemTemplate Template;

    public Item(ItemTemplate template)
    {
        Template = template;
    }

    public Item()
    {

    }

    public void AddAmount(int amountToAdd)
    {
        Amount += amountToAdd;
    }

    public void RemoveAmount(int amountToRemove)
    {
        Amount = Mathf.Max(0,Amount- amountToRemove);
    }

    public virtual void UpdateItemBehaviour()
    {

    }

    public virtual void OnEquipItem()
    {
        Debug.Log("On Equip Item " + Template.Name);
    }

    public virtual void OnUnequipItem()
    {
        Debug.Log("On Unequip Item " + Template.Name);
    }
}
