using UnityEngine;

namespace RuntimeScriptField.Example
{
	public class Example : MonoBehaviour
	{
		[Header("You can assign any Component here")]
		public ComponentReference component;
		[Header("You can only assing things inheriting from Foo here")]
		public FooReference foo;
		[Header("You can only assing things inheriting from Bar here")]
		public BarReference bar;

		void Start()
		{
			gameObject.AddComponent(component.script);
		}
	}

	[System.Serializable]
	public class FooReference : ScriptReference<Foo> { }

	//I guess somebody might do this?
	public class What_The_Face_Dude<T> : ScriptReference<T> { }

	[System.Serializable]
	public class BarReference : What_The_Face_Dude<Bar> { }
}