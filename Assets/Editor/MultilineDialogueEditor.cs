using System;
using UI.Dialogue;
using UnityEditor;

namespace Editor {
    [CustomEditor(typeof(MultilineDialogueComponent))]
    public class MultilineDialogueEditor : UnityEditor.Editor {
        private SerializedProperty dialogues;

        private void OnEnable() {
            dialogues = serializedObject.FindProperty("dial.dialogues");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(dialogues);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
