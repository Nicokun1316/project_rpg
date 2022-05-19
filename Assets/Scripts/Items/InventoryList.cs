using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "inventory", menuName = "Items/Inventory List", order = 0)]
    public class InventoryList : ScriptableObject {
        public List<Item> items;

        private void OnEnable() {
            items = new List<Item>();
        }
    }
}
