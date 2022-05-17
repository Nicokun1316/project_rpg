using UnityEngine;

namespace UI {
    public interface ActionButton : Focusable {
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
            PerformAction();
            return ConfirmResult.Return;
        }

        void Focusable.Unfocus() {
            
        }

        void Focusable.Freeze() {
        }

        void Focusable.Unfreeze() {
        }

        void PerformAction();
    }
}
