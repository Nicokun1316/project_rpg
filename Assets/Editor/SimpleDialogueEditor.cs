using System;
using UI.Dialogue;
using UnityEditor;

namespace Editor {
    [CustomEditor(typeof(SimpleDialogueComponent))]
    public class SimpleDialogueEditor : UnityEditor.Editor {
        private SerializedProperty text;

        private void OnEnable() {
            text = serializedObject.FindProperty("dial.text");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.PropertyField(text);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
