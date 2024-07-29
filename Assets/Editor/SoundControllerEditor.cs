using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundController))]
public class SoundControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SoundController soundController = (SoundController)target;
        SerializedProperty soundsProperty = serializedObject.FindProperty("sounds");

        // Display the array size field
        EditorGUILayout.PropertyField(soundsProperty);

        if (soundsProperty.isArray && soundsProperty.arraySize > 0)
        {
            for (int i = 0; i < soundsProperty.arraySize; i++)
            {
                SerializedProperty soundProperty = soundsProperty.GetArrayElementAtIndex(i);
                SerializedProperty clipProperty = soundProperty.FindPropertyRelative("clip");
                SerializedProperty pitchProperty = soundProperty.FindPropertyRelative("pitch");

                EditorGUILayout.PropertyField(clipProperty);
                EditorGUILayout.Slider(pitchProperty, 0.1f, 3.0f, new GUIContent("Pitch"));
            }
        }

        // Ensure the array size change creates new default elements
        if (GUILayout.Button("Add Sound"))
        {
            int newIndex = soundsProperty.arraySize;
            soundsProperty.arraySize++;
            SerializedProperty newSoundProperty = soundsProperty.GetArrayElementAtIndex(newIndex);
            newSoundProperty.FindPropertyRelative("pitch").floatValue = 1.0f;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
