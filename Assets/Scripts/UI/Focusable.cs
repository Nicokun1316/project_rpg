using UnityEngine;
using Object = System.Object;

namespace UI {
    public interface Focusable {
        ConfirmResult MoveInput(Vector2 direction);
        ConfirmResult Confirm();
        ConfirmResult Cancel();
        ConfirmResult Focus();

        Object State() {
            return null;
        }
        
        void Unfocus();
        void Freeze();
        void Unfreeze();

        bool ShouldPop() {
            return false;
        }
    }
}
