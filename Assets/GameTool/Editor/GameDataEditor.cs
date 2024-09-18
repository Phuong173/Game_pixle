using UnityEditor;
using UnityEngine;

namespace GameTool.Editor
{
    [CustomEditor(typeof(GameData))]
    public class GameDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            GameData gameData = (GameData) target;
            
            if (GUILayout.Button("Save Data"))
            {
                gameData.Save();
            }
            
            if (GUILayout.Button("Load Data"))
            {
                gameData.Load();
            }
            
            if (GUILayout.Button("Clear Data"))
            {
                gameData.ClearData();
            }

            base.OnInspectorGUI();
        }
    }
}