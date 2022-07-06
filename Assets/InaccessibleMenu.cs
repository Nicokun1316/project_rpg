using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UI.Dialogue;
using UnityEngine;

public class InaccessibleMenu : MonoBehaviour, Focusable {
    public ConfirmResult MoveInput(Vector2 direction) {
        return ConfirmResult.DoNothing;
    }

    public ConfirmResult Confirm() {
        return ConfirmResult.DoNothing;
    }

    public ConfirmResult Cancel() {
        return ConfirmResult.DoNothing;
    }

    public ConfirmResult Focus() {
        OpenDialogue().Forget();
        return ConfirmResult.Return;
    }

    public void Unfocus() {
    }

    public void Freeze() {
    }

    public void Unfreeze() {
    }

    private async UniTaskVoid OpenDialogue() {
        await UniTask.NextFrame();
        await UIManager.INSTANCE.PerformDialogue(new DialogueChunk(null, "This feature isn't unlocked yet."));
    }
}
