using UnityEngine;

namespace RuntimeScriptField.Inner {
	/// <summary>
	/// This only exists so we can have a drawer for all ScriptReference<T>'s. Don't inherit it,
	/// inherit from ScriptReference<T>!
	/// </summary>
	public abstract class ScriptReference_Base
	{
		[HideInInspector]
		public SerializableSystemType script;
	}
}