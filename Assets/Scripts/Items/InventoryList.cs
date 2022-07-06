using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "inventory", menuName = "Items/Inventory List", order = 0)]
    public class InventoryList : ScriptableObject, IEnumerable<Item> {
        private List<InventoryEntry> items;
        private int dollars;
        public int Dollars => dollars;

        private void OnEnable() {
            items = new List<InventoryEntry>();
        }

        public IEnumerator<Item> GetEnumerator() {
            return items.Select(i => i.item).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return items.GetEnumerator();
        }

        public bool HasItem(Item item) => this.Contains(item);

        public void AddItem(Item item) => items.Add(new InventoryEntry {equippedBy = CharacterType.None, item = item, slot = EquipmentSlot.None});

        public void RemoveItem(Item item) {
            var i = items.FindIndex(i => i.item == item);
            if (i >= 0) {
                items.RemoveAt(i);
            }
        }

        public void Equip(int index, CharacterType character, EquipmentSlot slot) {
            var item = items[index].item;
            items[index] = new InventoryEntry {
                item = item,
                equippedBy = character,
                slot = slot
            };
        }

        public void Unequip(CharacterType character, EquipmentSlot slot) {
            var index = items.FindIndex(item => item.slot == slot && item.equippedBy == character);
            if (index >= 0) {
                var item = items[index].item;
                items[index] = new InventoryEntry {
                    item = item,
                    equippedBy = CharacterType.None,
                    slot = EquipmentSlot.None
                };
            }
        }

        public bool AddMoney(int dollars) {
            if (dollars < 0) return false;
            this.dollars += dollars;
            return true;
        }

        public bool SubtractMoney(int dollars) {
            if (dollars > 0 || !CanAfford(dollars)) return false;
            this.dollars -= dollars;
            return true;
        }

        public bool CanAfford(int cost) => dollars >= cost;

        private static InventoryList SharedBag;

        public static InventoryList GetSharedBag() {
            if (SharedBag == null)
                SharedBag = Resources.FindObjectsOfTypeAll<InventoryList>().First(it => it.name == "shared_bag");
            return SharedBag;
        }
    }

    [Serializable]
    struct InventoryEntry {
        public CharacterType equippedBy;
        public Item item;
        public EquipmentSlot slot;
    }
}
