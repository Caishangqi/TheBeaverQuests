#if UNITY_EDITOR
using Core.Game.QuestManager.Data;
using UnityEditor;

namespace Core.Game.QuestManager.Editor
{
    [CustomEditor(typeof(PickUpWoodQuest))]
    public class PickUpWoodQuestEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            PickUpWoodQuest quest = (PickUpWoodQuest)target;

            DrawPropertiesExcluding(serializedObject, "m_Script", "canPickAnyMountOfWoodToCompleteQuest",
                "amountOfWoodToComplete");

            EditorGUILayout.PropertyField(serializedObject.FindProperty("canPickAnyMountOfWoodToCompleteQuest"));

            if (!quest.canPickAnyMountOfWoodToCompleteQuest)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("amountOfWoodToComplete"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif