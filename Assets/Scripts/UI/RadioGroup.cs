using System;
using UnityEngine;

namespace UI {
    public class RadioGroup : MenuChoiceComponent {
        public delegate void SelectionChanged(UIMenuItem selection);

        public event SelectionChanged onSelectionChanged;
        public override void Freeze() {
            base.Freeze();
        }

        public override void Unfreeze() {
            base.Unfreeze();
        }

        public override ConfirmResult MoveInput(Vector2 direction) {
            base.MoveInput(direction);
            onSelectionChanged?.Invoke(Choice.currentSelectedMenuItem);
            
            return ConfirmResult.DoNothing;
        }

        public override ConfirmResult Confirm() {
            return ConfirmResult.DoNothing;
        }

        public override ConfirmResult Cancel() {
            return ConfirmResult.DoNothing;
        }
        
    }
}
