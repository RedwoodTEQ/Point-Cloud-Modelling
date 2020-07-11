using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Unity3DViewer.UI
{
    public abstract class UIComponent<T> : MonoBehaviour
    {
        public Text label;
        public T component 
        {
            get {
                if(_component == null) {
                    _component = GetComponent<T>();
                }
                return _component;
            }
        }

        private T _component;
    }
}