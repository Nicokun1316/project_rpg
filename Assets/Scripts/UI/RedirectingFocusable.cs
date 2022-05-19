using UnityEngine;

namespace UI {
    public interface RedirectingFocusable : Focusable {
        protected Focusable target { get; }

        protected virtual void InitializeTarget(Focusable tar) {
            
        }

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
            var tar = target;
            InitializeTarget(tar);
            return ConfirmResult.ChangeFocus(tar);
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
