using Molioo.Simulator.Items;
using UnityEngine;

public class ItemEquippedRequirement : Requirement
{

    [SerializeField]
    private ItemTemplate _itemToCheck = null;

    [SerializeField]
    private bool _requiredEquipped = true;

    public override bool IsRequirementMet()
    {
        return PlayerSystemsManager.Instance.InventoryManager.IsItemEquipped(_itemToCheck) == _requiredEquipped;
    }

}
