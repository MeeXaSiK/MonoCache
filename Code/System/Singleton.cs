// ----------------------------------------------------------------
// The MIT License
// Singleton for Unity https://github.com/MeeXaSiK/NightSingleton
// Copyright (c) 2021-2022 Night Train Code
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
            return GetInstance(true);
        }

        public static TSingleton GetNotNull()
        {
            return GetInstance(false);
        }

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
                Debug.LogError($"The number of {typeof(TSingleton).Name} on the scene is greater than one!");
            }
            
            for (var i = 1; i < allInstances.Length; i++)
            {
                Destroy(allInstances[i]);
            }

            return cachedInstance = instance;
        }

        private static TSingleton GetInstanceIfNotFound(bool canBeNull)
        {
            return canBeNull 
                ? null 
                : new GameObject($"[Singleton] {typeof(TSingleton).Name}").AddComponent<TSingleton>();
        }
    }
}