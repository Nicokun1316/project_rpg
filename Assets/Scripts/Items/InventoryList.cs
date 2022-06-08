using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "inventory", menuName = "Items/Inventory List", order = 0)]
    public class InventoryList : ScriptableObject, IEnumerable<Item> {
        public List<Item> items;

        private void OnEnable() {
            items = new List<Item>();
        }

        public IEnumerator<Item> GetEnumerator() {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return items.GetEnumerator();
        }
    }
}
