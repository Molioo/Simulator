using UnityEngine;

public class TrashObject : ObjectDeactivatedWithProgress
{
    public void OnTrashPicked()
    {
        TrashBagItem trashBag = PlayerSystemsManager.Instance.InventoryManager.CurrentItem as TrashBagItem;
        if (trashBag!=null)
        {
            trashBag.TrashCollected();
        }
        IncreaseProgressStep();
    }
}
