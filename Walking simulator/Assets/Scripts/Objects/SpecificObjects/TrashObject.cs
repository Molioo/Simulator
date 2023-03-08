using UnityEngine;

public class TrashObject : MonoBehaviour
{
    public void OnTrashPicked()
    {
        TrashBagItem trashBag = PlayerSystemsManager.Instance.InventoryManager.CurrentItem as TrashBagItem;
        if(trashBag!=null)
        {
            trashBag.TrashCollected();
        }
    }
}
