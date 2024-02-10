// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2024 Night Train Code
// -------------------------------------------------------------------------------------------

#if DEBUG
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NTC.MonoCache
{
    internal static class MonoCacheExceptionsDetector
    {
        private const string OnEnableMethodName = "OnEnable";
        private const string OnDisableMethodName = "OnDisable";
        private const string UpdateMethodName = "Update";
        private const string FixedUpdateMethodName = "FixedUpdate";
        private const string LateUpdateMethodName = "LateUpdate";
        
        private const BindingFlags MethodFlags = BindingFlags.Public | 
                                                 BindingFlags.NonPublic | 
                                                 BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly;
        
        public static void CheckForExceptions()
        {
            var targetMethodNames = new string[]
            {
                OnEnableMethodName, OnDisableMethodName, UpdateMethodName, FixedUpdateMethodName, LateUpdateMethodName
            };
            var monoCacheType = typeof(MonoCache);
            var derivedTypes = Assembly
                .GetAssembly(monoCacheType)
                .GetTypes()
                .Where(type => type != monoCacheType && monoCacheType.IsAssignableFrom(type));
            
            foreach (var type in derivedTypes)
            {
                var methods = type.GetMethods(MethodFlags);

                foreach (var targetMethodName in targetMethodNames)
                {
                    foreach (var method in methods)
                    {
                        if (method.Name == targetMethodName)
                        {
                            Debug.LogError($"You are using the basic Unity method <{targetMethodName}> " +
                                           $"in <{type.Name}>! Use the analogue from <{nameof(MonoCache)}>");
                        }
                    }
                }
            }
        }
    }
}
#endif