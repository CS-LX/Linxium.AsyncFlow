using System.IO;
using Linxium.AsyncFlow.Storage.Specifics;
using UnityEditor;
using UnityEngine;

namespace Linxium.AsyncFlow.Editor {
    [CustomEditor(typeof(AsyncFlowContextSO))]
    public class AsyncFlowContextSOEditor : UnityEditor.Editor {
        SerializedProperty contextProp;
        const string DefaultPath = "Assets/Resources/";

        void OnEnable() {
            contextProp = serializedObject.FindProperty("context");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            // 使用默认的绘制
            EditorGUILayout.PropertyField(contextProp, true);

            // 添加 Save To Json 和 Load From Json 按钮
            GUILayout.Space(10);

            // Save To Json 按钮
            if (GUILayout.Button("Save To Json")) {
                SaveToJson();
            }

            // Load From Json 按钮
            if (GUILayout.Button("Load From Json")) {
                LoadFromJson();
            }
            serializedObject.ApplyModifiedProperties();
        }

        // 保存到 JSON 文件
        void SaveToJson() {
            string path = EditorUtility.SaveFilePanel("Save AsyncFlowContext as JSON", DefaultPath, target.name, "json");
            if (string.IsNullOrEmpty(path)) return;
            var context = ((AsyncFlowContextSO)target).Context;
            string json = AsyncFlowContext.ToJson(context);

            // 将JSON写入文件
            File.WriteAllText(path, json);

            // 刷新资产数据库以便 Unity 可以看到新文件
            AssetDatabase.Refresh();
            Debug.Log($"Saved AsyncFlowContext to: {path}");
        }

        // 从 JSON 文件加载
        void LoadFromJson() {
            string path = EditorUtility.OpenFilePanel("Load AsyncFlowContext from JSON", DefaultPath, "json");
            if (string.IsNullOrEmpty(path)) return;
            string json = File.ReadAllText(path);
            var context = AsyncFlowContext.FromJson(json);

            // 更新 SO 数据
            ((AsyncFlowContextSO)target).SetContext(context);

            // 刷新 Unity 以显示新的数据
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            Debug.Log($"Loaded AsyncFlowContext from: {path}");
        }
    }
}