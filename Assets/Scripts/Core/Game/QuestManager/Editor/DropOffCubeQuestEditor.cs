#if UNITY_EDITOR
using Core.Game.QuestManager.Data;
using UnityEditor;

namespace Core.Game.QuestManager.Editor
{
    [CustomEditor(typeof(DropOffCubeQuest))]
    public class DropOffCubeQuestEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DropOffCubeQuest quest = (DropOffCubeQuest)target;

            DrawPropertiesExcluding(serializedObject, "m_Script", "canDropCubeToAnyPositionCompleteQuest",
                "targetDropCubePosition",
                "positionTolerance", "markedTheTargetDropLocation", "cubeMarkerPrefab");

            EditorGUILayout.PropertyField(serializedObject.FindProperty("canDropCubeToAnyPositionCompleteQuest"));

            if (!quest.canDropCubeToAnyPositionCompleteQuest)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("targetDropCubePosition"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("positionTolerance"));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("markedTheTargetDropLocation"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("cubeMarkerPrefab"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif