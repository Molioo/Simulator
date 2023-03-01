using Molioo.Simulator.Items;
using UnityEngine;

public class DoesNotHaveItemRequirement : Requirement
{
    [SerializeField]
    private ItemTemplate _itemToHave = null;

    [SerializeField]
    private int _amountToHave = 1;

    public override bool IsRequirementMet()
    {
        return HasEnoughOfThisItem();
    }

    private bool HasEnoughOfThisItem()
    {
        return !PlayerSystemsManager.Instance.InventoryManager.HasGivenAmountOfItem(_itemToHave, _amountToHave);
    }
}
