// ----------------------------------------------------------------
// The MIT License
// Singleton for Unity https://github.com/MeeXaSiK/NightSingleton
// Copyright (c) 2021 Night Train Code
// ----------------------------------------------------------------

using UnityEngine;

namespace NTC.Global.System
{
    public class Singleton<TSingleton> : MonoBehaviour where TSingleton : MonoBehaviour
    {
        public static TSingleton Instance => GetNotNull();
        private static TSingleton cachedInstance;

        public static TSingleton GetCanBeNull()
        {
            if (cachedInstance != null)
            {
                return cachedInstance;
            }
            
            var instances = FindObjectsOfType<TSingleton>();
            var instance = instances.Length > 0 ? instances[0] : null;

            for (var i = 1; i < instances.Length; i++)
            {
                Destroy(instances[i]);
            }

            return cachedInstance = instance;
        }

        public static TSingleton GetNotNull()
        {
            if (cachedInstance != null)
            {
                return cachedInstance;
            }
            
            var instances = FindObjectsOfType<TSingleton>();
            var instance = instances.Length > 0 
                ? instances[0] 
                : new GameObject($"[Singleton] {typeof(TSingleton).Name}").AddComponent<TSingleton>();

            for (var i = 1; i < instances.Length; i++)
            {
                Destroy(instances[i]);
            }

            return cachedInstance = instance;
        }
    }
}