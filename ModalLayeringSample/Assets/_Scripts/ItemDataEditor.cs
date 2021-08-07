using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System;

namespace CodeSampleModalLayer
{
    [CustomEditor(typeof(ItemDataHandler))]
    public class ItemDataEditor : Editor
    {
        public bool makeJSONPretty = default;
        private SerializedProperty itemListProperty = default;
        private static GUIStyle titleStyle = default;
        private const string kFilePath = "Assets/Resources/ItemData.json";

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
            itemListProperty = serializedObject.FindProperty("data");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("<color=white>Item Data</color>", titleStyle);
            EditorGUILayout.Space();
            GUILayout.Space(20);

            makeJSONPretty = EditorGUILayout.Toggle("Format JSON", makeJSONPretty);
            if (GUILayout.Button("Preview JSON In Console", buttonOptions))
            {
                OnPreviewJSON();
            }

            if (GUILayout.Button("Validate Data", buttonOptions))
            {
                OnValidateData();
            }

            if (GUILayout.Button("Update Item Data File", buttonOptions))
            {
                OnUpdateData();
            }

            GUILayout.Space(20);
            serializedObject.Update();
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnUpdateData()
        {
            var animalListData = (ItemDataHandler)itemListProperty.serializedObject.targetObject;
            var jsonData = animalListData.CreateJsonStringFromData(makeJSONPretty: makeJSONPretty);

            StreamWriter sw = new StreamWriter(kFilePath);
            sw.Write(jsonData);
            sw.Close();

            EditorUtility.DisplayDialog(
                "Done!",
                "Data has been updated in ItemData.json file",
                "OK"
            );
            AssetDatabase.Refresh();
        }

        private void OnValidateData()
        {
            var animalListData = (ItemDataHandler)itemListProperty.serializedObject.targetObject;
            var totalErrors = animalListData.ValidateData();

            if (totalErrors > 0)
            {
                EditorUtility.DisplayDialog(
                    "Errors found!",
                    $"There are some errors found in the data. Check the console for more information.\n Number of errors found: {totalErrors}",
                    "OK"
                );
                return;
            }

            EditorUtility.DisplayDialog(
                "Data is valid!",
                $"Data is good to go. No errors found!",
                "OK"
            );
        }

        // Prints the JSON string to the console
        private void OnPreviewJSON()
        {
            var itemListData = (ItemDataHandler)itemListProperty.serializedObject.targetObject;
            var jsonData = itemListData.CreateJsonStringFromData(makeJSONPretty: makeJSONPretty);
            Debug.Log($"{jsonData}");
        }
    }
}