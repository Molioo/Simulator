using UnityEngine;

namespace Molioo.Simulator.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Molioo/Items/New Item")]
    public class Item : ScriptableObject
    {
        public string Name;
    }
}