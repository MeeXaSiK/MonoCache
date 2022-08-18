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
                    return GetInstance(false);
                }
            }
        }

        /// <summary>
        /// Returns the target instance, ignoring thread safety
        /// </summary>
        public static TSingleton InstanceNonLock => GetInstance(false);

        /// <summary>
        /// Can return a null instance if target one not found
        /// </summary>
        /// <returns> Target instance or null if not found </returns>
        public static TSingleton GetCanBeNull() => GetInstance(true);

        /// <summary>
        /// Will create the new instance if target one not found
        /// </summary>
        /// <returns> Target instance </returns>
        public static TSingleton GetNotNull() => GetInstance(false);
        
        /// <summary>
        /// Field for cached instance
        /// </summary>
        private static TSingleton cachedInstance;
        
        /// <summary>
        /// Object for thread safety
        /// </summary>
        private static readonly object SecurityLock = new object();

        /// <summary>
        /// Main method to get target instance
        /// </summary>
        /// <param name="canBeNull"> Can return a nullable instance </param>
        /// <returns> Target instance </returns>
        private static TSingleton GetInstance(bool canBeNull)
        {
            if (cachedInstance != null)
            {
                return cachedInstance;
            }
            
            var allInstances = FindObjectsOfType<TSingleton>();
            var instance = allInstances.Length > 0
                ? allInstances[0]
                : GetInstanceIfNotFound(canBeNull);
            
            if (allInstances.Length > 1)
            {
                for (var i = 1; i < allInstances.Length; i++)
                {
                    Destroy(allInstances[i]);
                }
#if DEBUG
                Debug.LogError($"The number of {typeof(TSingleton).Name} on the scene is greater than one!");
#endif
            }

            return cachedInstance = instance;
        }

        /// <summary>
        /// Decides what to do if the instance is not found
        /// </summary>
        /// <param name="canBeNull"> Can return a nullable instance if true </param>
        /// <returns> Null or new instance </returns>
        private static TSingleton GetInstanceIfNotFound(bool canBeNull)
        {
            return canBeNull 
                ? null 
                : new GameObject($"[Singleton] {typeof(TSingleton).Name}").AddComponent<TSingleton>();
        }
    }
}