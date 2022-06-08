using Skills;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomPropertyDrawer(typeof(Cost))]
    public class SkillCostEditor : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var valueRect = new Rect(position.x, position.y, 30, position.height);
            var typeRect = new Rect(position.x + 35, position.y, 70, position.height);
            var scalingRect = new Rect(position.x + 110, position.y, position.width - 110, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);
            EditorGUI.PropertyField(scalingRect, property.FindPropertyRelative("scaling"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
