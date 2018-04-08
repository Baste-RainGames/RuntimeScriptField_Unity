using System;
using UnityEngine;

namespace RuntimeScriptField
{
    /// <summary>
    /// You can assign a script that references a Component with this type, and add it at runtime.
    /// </summary>
    [Serializable]
    public class ComponentReference : ScriptReference<Component>
    {
        public void AddTo(GameObject gameObject)
        {
            gameObject.AddComponent(script);
        }

        public void AddTo(Component component)
        {
            component.gameObject.AddComponent(script);
        }
    }
}