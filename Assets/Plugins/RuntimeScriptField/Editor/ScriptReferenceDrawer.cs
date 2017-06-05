using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RuntimeScriptField.Inner
{
	[CustomPropertyDrawer(typeof(ScriptReference_Base), true)]
	public class ScriptReferenceDrawer : PropertyDrawer
	{
		private static bool monoScriptsCached;
		private static Dictionary<Type, MonoScript> typeToScript;

		private bool scriptreferenceCached;
		private ScriptReference_Base scriptReference;
		private SerializableSystemType script;
		private MonoScript monoScript;
		private Type requiredReferenceType;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!monoScriptsCached)
			{
				CacheMonoScripts();
			}

			if (!scriptreferenceCached)
			{
				CacheScriptReferenceData(property);
			}

			EditorGUI.BeginChangeCheck();
			var oldMonoScript = monoScript;
			monoScript = (MonoScript) EditorGUI.ObjectField(position, label, monoScript, typeof(MonoScript), false);
			if (EditorGUI.EndChangeCheck())
			{
				var systemType = monoScript == null ? null : monoScript.GetClass();

				if (IsValidScriptType(systemType, monoScript))
				{
					scriptReference.script = systemType == null ? null : new SerializableSystemType(systemType);

					var targetObj = property.serializedObject.targetObject;
					EditorUtility.SetDirty(targetObj);

					var asGameObj = targetObj as GameObject;
					if (asGameObj != null)
						EditorSceneManager.MarkSceneDirty(asGameObj.scene);
				}
				else
				{
					monoScript = oldMonoScript;
				}
			}
		}

		private bool IsValidScriptType(Type attemptedType, MonoScript attemptedScript)
		{
			if (attemptedType == null)
			{
				//user selected "None" when clicking the nipple. 
				if (attemptedScript == null)
					return true;
				
				Debug.LogWarning("Unity can't find the type inside the script " + attemptedScript.name + "! Can't assign it!");
				return false;
			}

			if (!requiredReferenceType.IsAssignableFrom(attemptedType))
			{
				Debug.LogWarningFormat("Cannot assign script {0} (containing the type {1}) to a {2}! The type of the script must derive from {3}!",
				                       attemptedScript.name, attemptedType, scriptReference.GetType().Name, requiredReferenceType.Name);
				return false;
			}

			if (script == null)
				return true;

			if (attemptedType != script.SystemType)
				return true;

			return false;
		}

		private static void CacheMonoScripts()
		{
			typeToScript = new Dictionary<Type, MonoScript>();
			var monoScripts = AssetDatabase.FindAssets("t:script")
			                               .Select(guid => AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(guid)));
			foreach (var monoScript in monoScripts)
			{
				var type = monoScript.GetClass();
				//Null for some types Unity can't handle for unknown reasons (like ScriptReference!)
				if (type != null)
				{
					typeToScript[type] = monoScript;
				}
			}

			monoScriptsCached = true;
		}

		private void CacheScriptReferenceData(SerializedProperty property)
		{
			scriptReference = (ScriptReference_Base) SerializedPropertyHelper.GetTargetObjectOfProperty(property);

			requiredReferenceType = GetGenericScriptRestriction(scriptReference.GetType());

			script = scriptReference.script;
			monoScript = null;
			if (script != null && script.SystemType != null)
			{
				typeToScript.TryGetValue(script.SystemType, out monoScript);
			}

			scriptreferenceCached = true;
		}

		private Type GetGenericScriptRestriction(Type subclassOfScriptReference)
		{
			var current = subclassOfScriptReference;

			var scriptReferenceType = typeof(ScriptReference<>);
			while (current != null)
			{
				if (!current.IsGenericType)
				{
					current = current.BaseType;
					continue;
				}

				var genericDef = current.GetGenericTypeDefinition();
				if (genericDef == scriptReferenceType)
				{
					return current.GetGenericArguments()[0];
				}

				current = current.BaseType;
			}

			return null;
		}
	}
}