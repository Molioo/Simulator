
public class HammerItem : Item
{
    public override void OnEquipItem()
    {
        base.OnEquipItem();
        gameObject.SetActive(true);
    }

    public override void OnUnequipItem()
    {
        base.OnUnequipItem();
        gameObject.SetActive(false);
    }
}
