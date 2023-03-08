
public class TrashBagItem : Item
{
    public int Capacity = 10;

    public int CurrentTrashCount = 0;

    public override bool CanBeUsed()
    {
        return CurrentTrashCount < Capacity;
    }

    public void TrashCollected()
    {
        CurrentTrashCount++;
    }
}
