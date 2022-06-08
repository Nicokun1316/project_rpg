using System;
using Skills;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Editor {
    [CustomPropertyDrawer(typeof(SkillEffect))]
    public class SkillEffectEditor : PropertyDrawer {
        private SkillEffectType? lastType = null;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            var nposition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var h = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("validTarget"));
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var typeRect = new Rect(nposition.x, nposition.y, 60, h);
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);
            var currentType = property.FindPropertyRelative("type").GetEnumValue<SkillEffectType>();
            lastType ??= currentType;
            if (lastType != currentType) {
                if (currentType == SkillEffectType.Attack)
                    property.FindPropertyRelative("validTarget").SetEnumValue(SkillTarget.Enemy);
                if (currentType == SkillEffectType.Healing)
                    property.FindPropertyRelative("validTarget").SetEnumValue(SkillTarget.Ally);
                lastType = currentType;
            }

            switch (currentType) {
                case SkillEffectType.Attack: {
                    var damageTypeRect = new Rect(nposition.x + 60, nposition.y, 60, h);
                    var validTargetRect = new Rect(nposition.x + 120, nposition.y, 60, h);
                    var aoeModeRect = new Rect(nposition.x + 180, nposition.y, 60, h);
                    var targetModeRect = new Rect(nposition.x + 240, nposition.y, 60, h);
                    EditorGUI.PropertyField(damageTypeRect, property.FindPropertyRelative("damageType"),
                        GUIContent.none);
                    EditorGUI.PropertyField(validTargetRect, property.FindPropertyRelative("validTarget"),
                        GUIContent.none);
                    EditorGUI.PropertyField(aoeModeRect, property.FindPropertyRelative("aoeMode"), GUIContent.none);
                    EditorGUI.PropertyField(targetModeRect, property.FindPropertyRelative("targetMode"),
                        GUIContent.none);
                    var nlpos = new Rect(position.x, nposition.y + h + 5, nposition.width, h);
                    var sPos = EditorGUI.PrefixLabel(nlpos, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Strength"));
                    var flatRect = new Rect(sPos.x, sPos.y, 60, h);
                    var scalingRect = new Rect(sPos.x + 60, sPos.y, 60, h);
                    var scalingTypeRect = new Rect(sPos.x + 120, sPos.y, 60, h);
                    EditorGUI.PropertyField(flatRect, property.FindPropertyRelative("strength.value"), GUIContent.none);
                    EditorGUI.PropertyField(scalingRect, property.FindPropertyRelative("strength.scaling"),
                        GUIContent.none);
                    EditorGUI.PropertyField(scalingTypeRect, property.FindPropertyRelative("strength.scalingType"), GUIContent.none);
                }
                    break;
                case SkillEffectType.Healing: {
                    var validTargetRect = new Rect(nposition.x + 60, nposition.y, 60, h);
                    var aoeModeRect = new Rect(nposition.x + 120, nposition.y, 60, h);
                    var targetModeRect = new Rect(nposition.x + 180, nposition.y, 60, h);
                    EditorGUI.PropertyField(validTargetRect, property.FindPropertyRelative("validTarget"),
                        GUIContent.none);
                    EditorGUI.PropertyField(aoeModeRect, property.FindPropertyRelative("aoeMode"), GUIContent.none);
                    EditorGUI.PropertyField(targetModeRect, property.FindPropertyRelative("targetMode"),
                        GUIContent.none);
                    var nlpos = new Rect(position.x, nposition.y + h + 5, nposition.width, h);
                    var sPos = EditorGUI.PrefixLabel(nlpos, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Strength"));
                    var flatRect = new Rect(sPos.x, sPos.y, 60, h);
                    var scalingRect = new Rect(sPos.x + 60, sPos.y, 60, h);
                    var scalingTypeRect = new Rect(sPos.x + 120, sPos.y, 60, h);
                    EditorGUI.PropertyField(flatRect, property.FindPropertyRelative("strength.value"), GUIContent.none);
                    EditorGUI.PropertyField(scalingRect, property.FindPropertyRelative("strength.scaling"),
                        GUIContent.none);
                    EditorGUI.PropertyField(scalingTypeRect, property.FindPropertyRelative("strength.scalingType"), GUIContent.none);
                }
                    break;
                case SkillEffectType.Utility: {
                    
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorGUI.indentLevel = indent;
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("validTarget")) * 2 + 5;
        }
    }
}
