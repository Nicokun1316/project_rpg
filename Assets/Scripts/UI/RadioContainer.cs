using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class RadioContainer : MonoBehaviour, Focusable {
    private RadioGroup grp;

    private RadioGroup group {
        get {
            if (grp == null) {
                grp = transform.GetComponentInChildren<RadioGroup>(true);
            }

            return grp;
        }
    }

    public ConfirmResult MoveInput(Vector2 direction) {
        group.MoveInput(direction);
        return ConfirmResult.DoNothing;
    }

    public ConfirmResult Confirm() {
        return ConfirmResult.DoNothing;
    }

    public ConfirmResult Cancel() {
        return ConfirmResult.DoNothing;
    }

    public ConfirmResult Focus() {
        return ConfirmResult.DoNothing;
    }

    public void Unfocus() {
        
    }

    public void Freeze() {
        group.Freeze();
    }

    public void Unfreeze() {
        group.Unfreeze();
    }
}
