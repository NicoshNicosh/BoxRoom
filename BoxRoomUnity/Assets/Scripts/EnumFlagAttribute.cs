using UnityEngine;

using System;
using UnityEditor;


#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
        Enum targetEnum = (Enum)Enum.ToObject(fieldInfo.FieldType, property.intValue);

        string propName = flagSettings.enumName;
        if (string.IsNullOrEmpty(propName))
            propName = ObjectNames.NicifyVariableName(property.name);

        EditorGUI.BeginChangeCheck();
        EditorGUI.BeginProperty(position, label, property);
        Enum enumNew = EditorGUI.EnumFlagsField(position, propName, targetEnum);
        if (!property.hasMultipleDifferentValues || EditorGUI.EndChangeCheck())
            property.intValue = (int)Convert.ChangeType(enumNew, targetEnum.GetType());

        EditorGUI.EndProperty();
    }
}
#endif


public class EnumFlagAttribute : PropertyAttribute
{
    public string enumName;

    public EnumFlagAttribute() {}

    public EnumFlagAttribute(string name)
    {
        enumName = name;
    }
}