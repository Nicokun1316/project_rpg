using System;
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
            try {
                var tar = target;
                if (tar == null) return ConfirmResult.Return;
                InitializeTarget(tar);
                return ConfirmResult.ChangeFocus(tar);
            } catch (NullReferenceException) {
                return ConfirmResult.Return;
            }
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
