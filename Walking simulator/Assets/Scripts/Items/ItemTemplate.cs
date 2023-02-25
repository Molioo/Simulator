using UnityEngine;

namespace Molioo.Simulator.Items
{
    [CreateAssetMenu(fileName = "New Item Template", menuName = "Molioo/Items/New Item Template")]
    public class ItemTemplate : ScriptableObject
    {
        public string ItemId;
        
        public string Name;

        public bool Stackable;

        public bool UsesAdditionalPrefab = false;

        public Item ItemPrefab = null;

        public bool ShouldBeSpawnedAsBoneChild;

        public HumanBodyBones BoneParent;

        public Vector3 LocalPositionInBone;

        public Vector3 LocalRotationInBone;
    }
}