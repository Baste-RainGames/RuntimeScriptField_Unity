# RuntimeScriptField_Unity
A utility for Unity allowing you to assign scripts in the editor, and add them to game objects at runtime.


# Usage
Copy Assets/Plugins into your own Assets/Plugins

Declare a field of the time ScriptReference, and use it at runtime:

```c#
public class Example : MonoBehaviour
{
    public ScriptReference scriptReference;
    
    void Start() {
        gameObject.AddComponent(scriptReference.script);
    }
}
```

You can drag-and-drop any script into the scriptReference field in the inspector.

# Why
There has been a lot of forum threads throughout the years where people ask for exactly something like this. I figured I might as well share a solution. 

# Contributions
Please! This is a very simple project, but if you want to add something like a type-safe generic version or something, go right ahead.

# Thanks to
Bryan Keiren for Serializable System Type: http://bryankeiren.com/files_public/UnityScripts/SerializableSystemType.cs
Dylan Engelman (lordofduct) for SerializedProperty tools: https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs
