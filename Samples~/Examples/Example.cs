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
        public FooReference fooScript;
        [Header("You can only assing things inheriting from Bar here")]
        public BarReference barScript;
        [Header("Works with lists!")]
        public List<ComponentReference> components;

        void Start()
        {
            var componentInstance = component.AddTo(gameObject);
            Debug.Log("Added component " + componentInstance);

            // You can also do gameObject.AddComponent(fooScript.script), but that returns the script as Component rather than Fo.
            var fooInstance = fooScript.AddTo(gameObject);
            fooInstance.LogFoo();

            var barInstance = barScript.AddTo(gameObject);
            barInstance.LogBar();

            foreach (var comp in components) {
                comp.AddTo(gameObject);
            }
        }
    }

    [System.Serializable]
    public class FooReference : ScriptReference<Foo> { }

    //Prove that this works:
    public class Generic_Class<T> : ScriptReference<T> where T : Component { }

    [System.Serializable]
    public class BarReference : Generic_Class<Bar> { }
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
