using System.Collections.Generic;
using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Equippable", order = 0)]
    public class EquippableItem : Item {
        public int health;
        public int defense;
        public int luck;
        public int speed;
        public int attack;
        public int magic;
        public EquippableType type;
        public CharacterType usableBy;
    }
}
