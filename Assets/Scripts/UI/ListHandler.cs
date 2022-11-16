using System;
using UnityEngine;

namespace UI {
    public class ListHandler<T> : MonoBehaviour, Focusable {
        private MenuChoice choice;

        public event Action<T> OnSelect;
        public event Action OnCancel;

        private void OnEnable() {
            choice = GetComponent<MenuChoice>();
        }

        public ConfirmResult MoveInput(Vector2 direction) {
            if (direction == Vector2.down) {
                choice.Next();
            } else if (direction == Vector2.up) {
                choice.Previous();
            }
            
            return ConfirmResult.DoNothing;
        }

        public ConfirmResult Confirm() {
            OnSelect?.Invoke(choice.currentSelectedMenuItem.GetComponent<GenericItemComponent<T>>().Item);
            return ConfirmResult.DoNothing;
        }

        public ConfirmResult Cancel() {
            OnCancel?.Invoke();
            return ConfirmResult.DoNothing;
        }

        public ConfirmResult Focus() {
            return ConfirmResult.DoNothing;
        }

        public void Unfocus() {
            Destroy(gameObject);
        }

        public void Freeze() {
        }

        public void Unfreeze() {
        }
    }
}
