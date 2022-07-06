using System;
using UI;
using UI.Dialogue;
using UnityEngine;

namespace Items {
    public class DialogueReward : MonoBehaviour {
        [SerializeField] private InventoryList inventory;
        [SerializeField] private Item item;
        [SerializeField] private bool granted = false;

        public Item Item => item;
        public bool Granted => granted;

        public static DialogueReward AddReward(GameObject gameObject, InventoryList inventory, Item item = null) {
            var reward = gameObject.AddComponent<DialogueReward>();
            reward.inventory = inventory;
            reward.item = item;
            return reward;
        }

        private void Start() {
            GetComponent<Dialogue>().AddFinishedListener(() => {
                if (!granted) {
                    inventory.AddItem(item);
                    granted = true;
                }
            });
        }
    }
}
