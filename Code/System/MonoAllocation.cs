using System.Collections.Generic;
using UnityEngine;
using static NTC.Global.System.NightSugar;

namespace NTC.Global.System
{
    public abstract class MonoAllocation : MonoBehaviour
    {
        private Dictionary<int, Component> _get;
        private Dictionary<int, Component[]> _gets;
        private Dictionary<int, Component> _childrenGet;
        private Dictionary<int, Component[]> _childrenGets;
        private Dictionary<int, Component> _parentGet;
        private Dictionary<int, Component[]> _parentGets;
        private Dictionary<int, Component> _find;
        private Dictionary<int, Component[]> _finds;

        public int GetID() => cachedInstanceId ??= GetInstanceID();
        private int? cachedInstanceId;

        public GameObject CachedGameObject => cachedGameObject ??= gameObject;
        private GameObject cachedGameObject;

        public Transform CachedTransform => cachedTransform ??= transform;
        private Transform cachedTransform;

        private bool allocationEnabled = true;

        public void EnableAllocation()
        {
            allocationEnabled = true;
        }

        public void DisableAllocation()
        {
            allocationEnabled = false;
        }

        public T Get<T>() => GetComponent<T>();
        
        public T[] Gets<T>() => GetComponents<T>();
        
        public T ChildrenGet<T>() => GetComponentInChildren<T>();
        
        public T[] ChildrenGets<T>() => GetComponentsInChildren<T>();
        
        public T ParentGet<T>() => GetComponentInParent<T>();
        
        public T[] ParentGets<T>() => GetComponentsInParent<T>();
        
        public T Find<T>() where T : Object => FindObjectOfType<T>();
        
        public T[] Finds<T>() where T : Object => FindObjectsOfType<T>();
        
        public T GetCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _get ??= new Dictionary<int, Component>(16);

                if (_get.TryGetValue(index, out var component))
                {
                    return (T) component;
                }
            }

            var instance = Get<T>();
            if (allocationEnabled) _get.Add(index, instance);

            return instance;
        }

        public T[] GetsCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _gets ??= new Dictionary<int, Component[]>(16);

                if (_gets.TryGetValue(index, out var component))
                {
                    return (T[]) component;
                }
            }

            var instance = Gets<T>();
            if (allocationEnabled) _gets.Add(index, instance);

            return instance;
        }

        public T ChildrenGetCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _childrenGet ??= new Dictionary<int, Component>(16);

                if (_childrenGet.TryGetValue(index, out var component))
                {
                    return (T) component;
                }
            }

            var instance = ChildrenGet<T>();
            if (allocationEnabled) _childrenGet.Add(index, instance);

            return instance;
        }

        public T[] ChildrenGetsCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _childrenGets ??= new Dictionary<int, Component[]>(16);

                if (_childrenGets.TryGetValue(index, out var component))
                {
                    return (T[]) component;
                }
            }

            var instance = ChildrenGets<T>();
            if (allocationEnabled) _childrenGets.Add(index, instance);

            return instance;
        }

        public T ParentGetCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _parentGet ??= new Dictionary<int, Component>(16);

                if (_parentGet.TryGetValue(index, out var component))
                {
                    return (T) component;
                }
            }

            var instance = ParentGet<T>();
            if (allocationEnabled) _parentGet.Add(index, instance);

            return instance;
        }

        public T[] ParentGetsCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _parentGets ??= new Dictionary<int, Component[]>(16);

                if (_parentGets.TryGetValue(index, out var component))
                {
                    return (T[]) component;
                }
            }

            var instance = ParentGets<T>();
            if (allocationEnabled) _parentGets.Add(index, instance);

            return instance;
        }

        public T FindCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _find ??= new Dictionary<int, Component>(16);

                if (_find.TryGetValue(index, out var component))
                {
                    return (T) component;
                }
            }

            var instance = Find<T>();
            if (allocationEnabled) _find.Add(index, instance);

            return instance;
        }

        public T[] FindsCached<T>() where T : Component
        {
            var index = GetInfo<T>.Index;

            if (allocationEnabled)
            {
                _finds ??= new Dictionary<int, Component[]>(16);

                if (_finds.TryGetValue(index, out var component))
                {
                    return (T[]) component;
                }
            }

            var instance = Finds<T>();
            if (allocationEnabled) _finds.Add(index, instance);

            return instance;
        }
    }
}