using System;
using System.Linq;
using Items;
using TMPro;
using UnityEngine;
using Utils;

namespace UI {
    public class ItemDescriptionComponent : MonoBehaviour, Focusable {
        private ItemComponent itemComponent;

        private TMP_Text description;

        private TMP_Text stats;

        private void Start() {
            itemComponent = gameObject.FindRecursive("Item").GetComponent<ItemComponent>();
            description = gameObject.FindRecursive("Description").GetComponent<TMP_Text>();
            stats = gameObject.FindRecursive("Stats").GetComponent<TMP_Text>();
        }

        public void SetItem(Item item) {
            itemComponent.item = item;
            description.text = item.description;
            try {
                var statString = item.EnumerateStats().Select(it =>
                        $"{it.name.ToString()}: {(it.value > 0 ? $"<color=green>+{it.value}</color>" : $"<color=red>{it.value}</color>")}")
                    .Aggregate((total, nextStat) => $"{total}\n{nextStat}");
                stats.text = statString;
            } catch (InvalidOperationException) {
                stats.text = "";
            }
        }

        public ConfirmResult MoveInput(Vector2 direction) {
            return direction == Vector2.down ? ConfirmResult.Return : ConfirmResult.DoNothing;
        }

        public ConfirmResult Confirm() {
            return ConfirmResult.Return;
        }

        public ConfirmResult Cancel() {
            return ConfirmResult.Return;
        }

        public ConfirmResult Focus() {
            gameObject.GetComponentInParent<Canvas>().enabled = true;
            return ConfirmResult.DoNothing;
        }

        public void Unfocus() {
            gameObject.GetComponentInParent<Canvas>().enabled = false;
        }

        public void Freeze() {
        }

        public void Unfreeze() {
        }
    }
}
