using Molioo.Simulator.Items;
using UnityEngine;

public class CanUseItemRequirement : Requirement
{
    [SerializeField]
    private ItemTemplate _itemToCheck = null;

    public override bool IsRequirementMet()
    {
        return PlayerSystemsManager.Instance.InventoryManager.CanUseItem(_itemToCheck);
    }

  
}
