using System;
using UI;
using UI.Dialogue;
using UnityEngine;

namespace Items {
    public class DialogueReward : MonoBehaviour {
        public InventoryList inventory;
        public Item item;
        [SerializeField] private bool granted = false;

        private void Start() {
            GetComponent<Dialogue>().AddFinishedListener(() => {
                if (!granted) {
                    inventory.items.Add(item);
                    granted = true;
                }
            });
        }
    }
}
