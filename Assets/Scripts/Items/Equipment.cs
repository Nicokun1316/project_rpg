using System.Collections.Generic;
using UnityEngine;

namespace Items {
    public class Equipment : MonoBehaviour {
        public EquippableItem weapon;
        public EquippableItem armour;
        public EquippableItem leftTrinket;
        public EquippableItem rightTrinket;

        public IEnumerator<EquippableItem> equippedItems() {
            if (weapon != null) yield return weapon;
            if (armour != null) yield return armour;
            if (leftTrinket != null) yield return leftTrinket;
            if (rightTrinket != null) yield return rightTrinket;
        }
    }
}
