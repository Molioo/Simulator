
using UnityEngine;

public class PlayerSystemsManager : Singleton<PlayerSystemsManager>
{
    public PlayerInventoryManager InventoryManager;
    public PlayerObjectMovingController ObjectMovingController;
    public Animator PlayerAnimator;
}
