using UnityEngine;
using UnityEditor;
using System.IO;


namespace CodeSampleModalLayer
{
    [CustomEditor(typeof(CopyHandler))]
    public class CopyDataEditor : Editor
    {
        public bool makeJSONPretty = default;
        private SerializedProperty copyListProperty = default;
        private static GUIStyle titleStyle = default;
        private const string kFilePath = "Assets/Resources/CopyData.json";

        private GUILayoutOption[] buttonOptions = new GUILayoutOption[] {
            GUILayout.Height(24)
        };

        void OnEnable()
        {
            titleStyle = new GUIStyle();
            titleStyle.fontSize = 24;
            titleStyle.richText = true;

            SetupItemList();
        }

        private void SetupItemList()
        {
            copyListProperty = serializedObject.FindProperty("data");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("<color=white>Copy Data</color>", titleStyle);
            EditorGUILayout.Space();
            GUILayout.Space(20);

            makeJSONPretty = EditorGUILayout.Toggle("Format JSON", makeJSONPretty);
            if (GUILayout.Button("Preview JSON In Console", buttonOptions))
            {
                OnPreviewJSON();
            }

            if (GUILayout.Button("Update Copy Data File", buttonOptions))
            {
                OnUpdateData();
            }

            GUILayout.Space(20);
            serializedObject.Update();
            base.OnInspectorGUI();
            copyListProperty.serializedObject.ApplyModifiedProperties();
        }

        private void OnUpdateData()
        {
            var copyListData = (CopyHandler)copyListProperty.serializedObject.targetObject;
            var jsonData = copyListData.CreateJsonStringFromData(makeJSONPretty: makeJSONPretty);

            StreamWriter sw = new StreamWriter(kFilePath);
            sw.Write(jsonData);
            sw.Close();

            EditorUtility.DisplayDialog(
                "Done!",
                "Data has been updated in CopyData.json file",
                "OK"
            );
            AssetDatabase.Refresh();
        }

        // Prints the JSON string to the console
        private void OnPreviewJSON()
        {
            var itemListData = (CopyHandler)copyListProperty.serializedObject.targetObject;
            var jsonData = itemListData.CreateJsonStringFromData(makeJSONPretty: makeJSONPretty);
            Debug.Log($"{jsonData}");
        }
    }
}