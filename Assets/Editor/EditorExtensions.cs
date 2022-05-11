using UnityEditor;
using UnityEngine;

namespace DefaultNamespace {
    public class EditorExtensions : MonoBehaviour {
        [MenuItem("GameObject/UI/Dialogue Text", false, 10)]
        static void CreateDialogueText(MenuCommand command) {
            var go = new GameObject("DialogueText");
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasRenderer>();
            var tmp = go.AddComponent<DialogueText>();
            tmp.fontSize = 16;
            tmp.text = "Sample text...";
            GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, $"Create {go.name}");
            Selection.activeObject = go;
        }
    }
}
