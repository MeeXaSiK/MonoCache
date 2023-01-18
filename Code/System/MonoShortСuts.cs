using UnityEngine;

namespace NTC.Global.System
{
    public abstract class MonoShortСuts : MonoBehaviour
    {
        public T Get<T>() => GetComponent<T>();
        
        public T[] Gets<T>() => GetComponents<T>();
        
        public T ChildrenGet<T>() => GetComponentInChildren<T>();
        
        public T[] ChildrenGets<T>() => GetComponentsInChildren<T>();
        
        public T ParentGet<T>() => GetComponentInParent<T>();
        
        public T[] ParentGets<T>() => GetComponentsInParent<T>();
        
        public T Find<T>() where T : Object => FindObjectOfType<T>();
        
        public T[] Finds<T>() where T : Object => FindObjectsOfType<T>();
    }
}