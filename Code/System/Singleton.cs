// ----------------------------------------------------------------
// The MIT License
// Singleton for Unity https://github.com/MeeXaSiK/NightSingleton
// Copyright (c) 2021-2022 Night Train Code
// ----------------------------------------------------------------

using UnityEngine;

namespace NTC.Global.System
{
    public abstract class Singleton<TSingleton> : MonoBehaviour where TSingleton : MonoBehaviour
    {
        /// <summary>
        /// Returns a target instance
        /// </summary>
        public static TSingleton Instance
        {
            get
            {
                lock (SecurityLock)
                {
                    return GetInstance();
                }
            }
        }

        /// <summary>
        /// Returns the target instance, ignoring thread safety
        /// </summary>
        public static TSingleton InstanceNonLock => GetInstance();
        
        /// <summary>
        /// Object for thread safety
        /// </summary>
        private static readonly object SecurityLock = new object();

        /// <summary>
        /// Field for cached instance
        /// </summary>
        private static TSingleton _cachedInstance;
        
        /// <summary>
        /// Main method to get target instance
        /// </summary>
        /// <returns> Target instance </returns>
        private static TSingleton GetInstance()
        {
            if (_cachedInstance != null)
            {
                return _cachedInstance;
            }
            
            var allInstances = FindObjectsOfType<TSingleton>();
            var className = typeof(TSingleton).Name;
            var count = allInstances.Length;
            var instance = count > 0
                ? allInstances[0]
                : new GameObject($"[Singleton] {className}").AddComponent<TSingleton>();
            
            if (count > 1)
            {
                for (var i = 1; i < count; i++)
                {
                    Destroy(allInstances[i]);
                }
#if DEBUG
                Debug.LogError($"The number of <{className}> on the scene is greater than one!");
#endif
            }

            return _cachedInstance = instance;
        }
    }
}