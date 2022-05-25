using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Utility", order = 0)]
    public class UtilityItem : Item {
        public int health;
        public int magic;
        public bool isConsumable;
    }
}
