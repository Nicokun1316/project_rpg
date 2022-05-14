using UnityEngine;

namespace UI {
    public interface Focusable {
        ConfirmResult MoveInput(Vector2 direction);
        ConfirmResult Confirm();
        ConfirmResult Cancel();
        ConfirmResult Focus();
        void Unfocus();
        void Freeze();
        void Unfreeze();

        bool ShouldPop() {
            return false;
        }
    }
}
