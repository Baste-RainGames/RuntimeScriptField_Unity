using RuntimeScriptField.Inner;
using UnityEngine;

namespace RuntimeScriptField
{
    /// <summary>
    /// Inherit this to create a field where you can assign a script at edit time, and get access to the script's
    /// type at runtime!
    /// </summary>
    /// <typeparam name="T">Type restriction for the script type. You can only assign scripts of the type T to the field</typeparam>
    public class ScriptReference<T> : ScriptReference_Base where T : Component {
        public T AddTo(GameObject gameObject)
        {
            return (T) gameObject.AddComponent(script);
        }

        public T AddTo(Component component)
        {
            return (T) component.gameObject.AddComponent(script);
        }
    }
}