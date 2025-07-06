using UnityEditor;
using UnityEngine;

namespace PurrNet.Editor
{
    [CustomPropertyDrawer(typeof(Reference<>))]
    public class ReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();


            var reference = property.FindPropertyRelative("_reference");
            const float iconWidth = 20f;
            const float iconPadding = 10f;
            EditorGUI.ObjectField(position, reference, label);

            var labelWidth = position.width - iconWidth - iconPadding;
            var iconRect = new Rect(position.x + labelWidth - iconPadding, position.y, iconWidth, position.height);

            GUI.Label(iconRect, EditorGUIUtility.IconContent("RelativeJoint2D Icon"));

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
