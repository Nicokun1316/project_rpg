using System;
using UnityEngine;

namespace UI {
    public class InventoryTabItem: MonoBehaviour, RedirectingFocusable {
        private ItemComponent item;

        private void Start() {
            item = GetComponent<ItemComponent>();
        }

        Focusable RedirectingFocusable.target => GameObject.FindGameObjectWithTag("ItemMenu").transform.Find("ItemMenu").GetComponent<Focusable>();
        void RedirectingFocusable.InitializeTarget(Focusable tar) {
            (tar as MonoBehaviour)?.GetComponent<ItemDescriptionComponent>().SetItem(item.item);
        }
    }
}
