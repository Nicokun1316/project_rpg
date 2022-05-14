using UnityEngine;

namespace UI {
    public interface RedirectingFocusable : Focusable {
        protected Focusable target { get; }

        ConfirmResult Focusable.MoveInput(Vector2 direction) {
            return ConfirmResult.DoNothing;
        }

        ConfirmResult Focusable.Confirm() {
            return ConfirmResult.DoNothing;
        }

        ConfirmResult Focusable.Cancel() {
            return ConfirmResult.DoNothing;
        }

        ConfirmResult Focusable.Focus() {
            return ConfirmResult.ChangeFocus(target);
        }

        void Focusable.Unfocus() { }

        void Focusable.Freeze() {
            
        }

        void Focusable.Unfreeze() {
            
        }

        bool Focusable.ShouldPop() {
            return true;
        }
    }
}
