using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Consumable", order = 0)]
    public class ConsumableItem : Item {
        public int health;
        public int magic;
    }
}
