using System;
using UnityEngine;
using Utils;

namespace UI {
    public class ScriptableMenuChoiceComponent : MonoBehaviour, Focusable {
        public MenuChoice Choice { get; private set; }
        [SerializeField] private bool isHorizontal = true;

        public Action OnSelect;
        public Action OnCancel;

        private void OnEnable() {
            Choice = GetComponent<MenuChoice>();
        }

        public void Dismiss() {
            UIManager.INSTANCE.PopFocus();
        }
        
        public virtual ConfirmResult MoveInput(Vector2 direction) {
            if (!isHorizontal) {
                var (x, y) = direction;
                direction = new(-y, -x);
            }

            if (direction == Vector2.right) {
                Choice.Next();
            } else if (direction == Vector2.left) {
                Choice.Previous();
            } else if (direction == Vector2.down) {
                Choice.currentSelectedMenuItem.GetComponent<Focusable>()?.MoveInput(Vector2.right);
            } else if (direction == Vector2.up) {
                Choice.currentSelectedMenuItem.GetComponent<Focusable>()?.MoveInput(Vector2.left);
            }

            return ConfirmResult.DoNothing;
        }

        public virtual ConfirmResult Confirm() {
            OnSelect();
            return ConfirmResult.DoNothing;
        }

        public virtual ConfirmResult Cancel() {
            OnCancel();
            return ConfirmResult.DoNothing;
        }

        public virtual ConfirmResult Focus() {
            gameObject.SetActive(true);
            Choice.index = 0;
            return ConfirmResult.DoNothing;
        }

        public virtual void Unfocus() {
            ((MonoBehaviour) Choice).gameObject.SetActive(false);
        }

        public virtual void Freeze() {
            Choice?.StopAnimation();
        }

        public virtual void Unfreeze() {
            Choice?.ResumeAnimation();
        }
    }
}
