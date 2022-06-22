using Items;
using UnityEngine;
using Utils;

namespace UI {
    public class MenuChoiceComponent : MonoBehaviour, Focusable {
        public MenuChoice choice { get; private set; }
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
                choice.currentSelectedMenuItem.GetComponent<Focusable>()?.MoveInput(Vector2.right);
            } else if (direction == Vector2.up) {
                choice.currentSelectedMenuItem.GetComponent<Focusable>()?.MoveInput(Vector2.left);
            }

            return ConfirmResult.DoNothing;
        }

        public virtual ConfirmResult Confirm() {
            print($"csmi = {choice.currentSelectedMenuItem}; focus = {choice.currentSelectedMenuItem?.GetComponent<Focusable>()}");
            var focusable = choice.currentSelectedMenuItem?.GetComponent<Focusable>();
            return focusable == null ? ConfirmResult.DoNothing : ConfirmResult.ChangeFocus(focusable);
        }

        public virtual ConfirmResult Cancel() {
            return ConfirmResult.Return;
        }

        public virtual ConfirmResult Focus() {
            gameObject.parent().SetActive(true);
            choice.index = 0;
            return ConfirmResult.DoNothing;
        }

        public virtual void Unfocus() {
            ((MonoBehaviour) choice).gameObject.parent().SetActive(false);
        }

        public virtual void Freeze() {
            choice?.StopAnimation();
        }

        public virtual void Unfreeze() {
            choice?.ResumeAnimation();
        }
    }
}
