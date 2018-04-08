using System.Collections.Generic;
using RuntimeScriptField;
using RuntimeScriptField.Example;
using UnityEngine;

namespace RuntimeScriptField.Example
{
    public class Example : MonoBehaviour
    {
        [Header("All scripts assigned under this will be added to the same gameobjects on play!")]
        [Header("You can assign any Component here")]
        public ComponentReference component;
        [Header("You can only assing things inheriting from Foo here")]
        public FooReference foo;
        [Header("You can only assing things inheriting from Bar here")]
        public BarReference bar;
        [Header("Works with lists!")]
        public List<ComponentReference> components;

        void Start()
        {
            gameObject.AddComponent(component.script);
            gameObject.AddComponent(foo.script);
            gameObject.AddComponent(bar.script);
            foreach (var comp in components) {
                gameObject.AddComponent(comp.script);
            }
        }
    }

    [System.Serializable]
    public class FooReference : ScriptReference<Foo> { }

    //I guess somebody might do this?
    public class What_The_Face_Dude<T> : ScriptReference<T> { }

    [System.Serializable]
    public class BarReference : What_The_Face_Dude<Bar> { }
}

[System.Serializable]
public class FooReference : ScriptReference<Foo> { }

public class Example2 : MonoBehaviour
{
    public FooReference fooRef;
    
    void Start() {
        Debug.Log("The script you assigned to fooRef is: " + fooRef.script);
    }
}
