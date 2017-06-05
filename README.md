# RuntimeScriptField_Unity
A utility for Unity allowing you to assign scripts in the editor, and add them to game objects at runtime.


# Usage
Copy Assets/Plugins into your own Assets/Plugins

Declare a field of the type ComponentReference, and use it at runtime:

```c#
using RuntimeScriptField;

public class Example : MonoBehaviour
{
    public ComponentReference scriptRef;
    
    void Start() {
        scriptRef.AddTo(gameObject);
    }
}
```

You can drag-and-drop any script into the scriptRef field in the inspector, as long as the script contains a Component.

If you want more or less control over what's assignable to the script, you can inherit from ScriptReference<T>:

```c#
using RuntimeScriptField;

[System.Serializable]
public class FooReference : ScriptReference<Foo> { }

public class Example2 : MonoBehaviour
{
    public FooReference fooRef;
    
    void Start() {
        Debug.Log("The script you assigned to fooRef is: " + fooRef.script);
    }
}
```

In this case, only scripts containing something that inherits from Foo can be assigned to the field. ComponentReference is simply a ScriptReference<Component> with some utility methods.

# Why
There has been a lot of forum threads throughout the years where people ask for exactly something like this. I figured I might as well share a solution. 

# Contributions
Please! This is a very simple project, but if you feel something's missing, please send a pull request!

# Bugs
Please report any bug you find!

# Thanks to
Bryan Keiren for Serializable System Type: http://bryankeiren.com/files_public/UnityScripts/SerializableSystemType.cs
Dylan Engelman (lordofduct) for SerializedProperty tools: https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs
