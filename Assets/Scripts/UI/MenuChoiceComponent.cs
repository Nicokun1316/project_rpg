using UnityEngine;
using Utils;

namespace UI {
    public class MenuChoiceComponent : MonoBehaviour, Focusable {
        private MenuChoice choice;

        private void OnEnable() {
            choice = GetComponent<MenuChoice>();
        }

        public ConfirmResult MoveInput(Vector2 direction) {
            if (direction == Vector2.right) {
                choice.Next();
            } else if (direction == Vector2.left) {
                choice.Previous();
            }

            return ConfirmResult.DoNothing;
        }

        public ConfirmResult Confirm() {
            return ConfirmResult.ChangeFocus(choice.currentSelection.GetComponent<Focusable>());
        }

        public ConfirmResult Cancel() {
            return ConfirmResult.Return;
        }

        public ConfirmResult Focus() {
            gameObject.parent().SetActive(true);
            choice.SetIndex(0);
            return ConfirmResult.DoNothing;
        }

        public void Unfocus() {
            choice.gameObject.parent().SetActive(false);
        }

        public void Freeze() {
            choice.StopAnimation();
        }

        public void Unfreeze() {
            choice.ResumeAnimation();
        }
    }
}
