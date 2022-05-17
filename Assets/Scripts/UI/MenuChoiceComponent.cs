using UnityEngine;
using Utils;

namespace UI {
    public class MenuChoiceComponent : MonoBehaviour, Focusable {
        protected MenuChoice choice;
        [SerializeField] private bool isHorizontal = true;

        private void OnEnable() {
            choice = GetComponent<MenuChoice>();
        }

        public virtual ConfirmResult MoveInput(Vector2 direction) {
            if (!isHorizontal) {
                var (x, y) = direction;
                direction = new(-y, -x);
            }

            if (direction == Vector2.right) {
                choice.Next();
            } else if (direction == Vector2.left) {
                choice.Previous();
            } else if (direction == Vector2.down) {
                choice.currentSelection.GetComponent<Focusable>().MoveInput(Vector2.right);
            } else if (direction == Vector2.up) {
                choice.currentSelection.GetComponent<Focusable>().MoveInput(Vector2.left);
            }

            return ConfirmResult.DoNothing;
        }

        public virtual ConfirmResult Confirm() {
            return ConfirmResult.ChangeFocus(choice.currentSelection.GetComponent<Focusable>());
        }

        public virtual ConfirmResult Cancel() {
            return ConfirmResult.Return;
        }

        public virtual ConfirmResult Focus() {
            gameObject.parent().SetActive(true);
            choice.SetIndex(0);
            return ConfirmResult.DoNothing;
        }

        public virtual void Unfocus() {
            choice.gameObject.parent().SetActive(false);
        }

        public virtual void Freeze() {
            choice.StopAnimation();
        }

        public virtual void Unfreeze() {
            choice.ResumeAnimation();
        }
    }
}
