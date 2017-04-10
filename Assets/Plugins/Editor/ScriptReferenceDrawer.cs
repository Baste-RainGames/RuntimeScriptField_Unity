using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ScriptReferenceInner
{
    [CustomPropertyDrawer(typeof(ScriptReference))]
    public class ScriptReferenceDrawer : PropertyDrawer
    {
        private static bool monoScriptsCached;
        private static Dictionary<Type, MonoScript> typeToScript;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!monoScriptsCached)
            {
                CacheMonoScripts();
            }

            var sr = (ScriptReference) SerializedPropertyHelper.GetTargetObjectOfProperty(property);
            var script = sr.script;
            MonoScript monoScript = null;
            if (script != null && script.SystemType != null)
                typeToScript.TryGetValue(script.SystemType, out monoScript);

            EditorGUI.BeginChangeCheck();
            monoScript = (MonoScript) EditorGUI.ObjectField(position, label, monoScript, typeof(MonoScript), false);
            if (EditorGUI.EndChangeCheck())
            {
                var systemType = monoScript.GetClass();

                if (script == null || systemType != script.SystemType)
                {
                    sr.script = new SerializableSystemType(systemType);

                    var targetObj = property.serializedObject.targetObject;
                    EditorUtility.SetDirty(targetObj);

                    var asGameObj = targetObj as GameObject;
                    if (asGameObj != null)
                        EditorSceneManager.MarkSceneDirty(asGameObj.scene);
                }
            }
        }

        private static void CacheMonoScripts()
        {
            typeToScript = new Dictionary<Type, MonoScript>();
            var monoScripts = AssetDatabase.FindAssets("t:script")
                                           .Select(guid => AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(guid)));
            foreach (var monoScript in monoScripts)
            {
                typeToScript[monoScript.GetClass()] = monoScript;
            }

            monoScriptsCached = true;
        }
    }
}