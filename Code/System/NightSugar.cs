using System;
using UnityEngine;

namespace NTC.Global.System
{
    public static class NightSugar
    {
        public static void IfNotNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component != null)
                action?.Invoke(component);
        }

        public static void IfNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component == null)
                action?.Invoke(component);
        }

        public static void Enable(this Component component)
        {
            Enable(component.gameObject);
        }

        public static void Enable(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        
        public static void Disable(this Component component)
        {
            Disable(component.gameObject);
        }
        
        public static void Disable(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        
        public static void EnableParent(this Component component)
        {
            var parent = component.transform.parent;
            
            if (parent == null)
                component.Enable();
            else
                parent.Enable();
        }

        public static void DisableParent(this Component component)
        {
            var parent = component.transform.parent;
            
            if (parent == null)
                component.Disable();
            else
                parent.Disable();
        }

        public static Transform TryGetParent(this Component component)
        {
            var transform = component.transform;
            var parent = transform.parent;
            return parent == null ? transform : parent;
        }

        public static Transform TryGetChild(this Component component)
        {
            var transform = component.transform;
            var children = transform.GetChild(0);
            return children == null ? transform : children;
        }
        
        public static T GetNearby<T>(this Component component) where T : Component
        {
            T instance = null;

            if (component.transform.parent != null)
                instance = component.GetComponentInParent<T>();

            if (instance == null)
                instance = component.GetComponentInChildren<T>();

            if (instance == null)
                throw new NullReferenceException(typeof(T).Name);

            return instance;
        }
    }
}