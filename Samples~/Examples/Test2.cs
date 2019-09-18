using UnityEngine;

namespace RuntimeScriptField.Example {
    public class Test2 : MonoBehaviour {

        public ComponentReference scriptRef;
        public FooReference fooReference;
        public BarReference barReference;

        void Start() {
            Component c = scriptRef.AddTo(gameObject);
            Foo f = fooReference.AddTo(gameObject);
            Bar b = barReference.AddTo(gameObject);
        }
    }
}