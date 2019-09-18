# RuntimeScriptField_Unity
A utility for Unity allowing you to assign scripts in the editor, and add them to game objects at runtime.

## Installation
The package branch is setup to be used as UPM package, simply add the following line as a dependency in your project manifest.
```json
"basteraingames.runtimescriptfieldunity": "https://github.com/Baste-RainGames/RuntimeScriptField_Unity.git#package",
```
If you're using an older version of Unity than 2018.4, you can copy the Runtime and Editor folders to your Unity project.

## Use
To use the project, declare a field of the type ComponentReference, and use it at runtime:

```c#
using RuntimeScriptField;

public class Example : MonoBehaviour
{
    public ComponentReference componentRef;
    
    void Start() {
        Component c = componentRef.AddTo(gameObject);
    }
}
```

You can drag-and-drop any script into the componentRef field in the inspector, as long as the script contains a Component. The AddTo method returns the added object, just like the built-in AddComponent

If you want more or less control over what's assignable to the script, you can inherit from ScriptReference<T>:

```c#
using RuntimeScriptField;

[System.Serializable]
public class FooReference : ScriptReference<Foo> { }

public class Example2 : MonoBehaviour
{
    public FooReference fooRef;
    
    void Start() {
        // the .script property returns the System.Type of the script
        Debug.Log("The script you assigned to fooRef is: " + fooRef.script);
        //Add it with AddTo, which is typed!
        Foo f = fooRef.AddTo(gameObject);
    }
}
```

In this case, only scripts containing something that inherits from Foo can be assigned to the field. Note that AddTo is generic, so you always get a Foo back from FooReference.AddTo. ComponentReference is simply a ScriptReference<Component>, provided for convenience.

## Why
There has been a lot of forum threads throughout the years where people ask for exactly something like this. I figured I might as well share a solution. 

# Contributions
Please! This is a very simple project, but if you feel something's missing, please send a pull request!

# Bugs
Please report any bug you find!

# Thanks to
Nicola Baribeau for doing the initial work to convert this to a UPM package.
Bryan Keiren for Serializable System Type: http://bryankeiren.com/files_public/UnityScripts/SerializableSystemType.cs
Dylan Engelman (lordofduct) for SerializedProperty tools: https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs