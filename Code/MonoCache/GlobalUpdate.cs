// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using NTC.Global.System;
using UnityEngine;

namespace NTC.Global.Cache
{
    [DisallowMultipleComponent]
    public sealed class GlobalUpdate : Singleton<GlobalUpdate>
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private const string OnEnable = nameof(OnEnable);
        private const string OnDisable = nameof(OnDisable);
        
        private const string UpdateName = nameof(Update);
        private const string FixedUpdateName = nameof(FixedUpdate);
        private const string LateUpdateName = nameof(LateUpdate);

        private const string WhiteColor = "FFFFFF";
        private const string BlueColor = "00FFF7";
        private const string OrangeColor = "F4CA16";
        private const string RedColor = "E22121";
        
        private const BindingFlags MethodFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly;

        private void Awake()
        {
            CheckForErrors();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void CheckForErrors()
        {
            var subclassTypes = Assembly
                .GetAssembly(typeof(MonoCache))
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(MonoCache)));
            
            foreach (var type in subclassTypes)
            {
                var methods = type.GetMethods(MethodFlags);
                
                foreach (var method in methods)
                {
                    if (method.Name == OnEnable)
                    {
                        Debug.LogException(new Exception(
                            $"{GetExceptionBaseText(OnEnable, type.Name)}" +
                            $"{GetColoredText(BlueColor, "protected override void")} " +
                            $"{GetColoredText(OrangeColor, "OnEnabled()")}"));
                    }

                    if (method.Name == OnDisable)
                    {
                        Debug.LogException(new Exception(
                            $"{GetExceptionBaseText(OnDisable, type.Name)}" +
                            $"{GetColoredText(BlueColor, "protected override void")} " +
                            $"{GetColoredText(OrangeColor, "OnDisabled()")}"));
                    }
                    
                    if (method.Name == UpdateName)
                    {
                        Debug.LogWarning(
                            GetWarningBaseText(
                                method.Name, "Run()", type.Name));
                    }
                    
                    if (method.Name == FixedUpdateName)
                    {
                        Debug.LogWarning(
                            GetWarningBaseText(
                                method.Name, "FixedRun()", type.Name));
                    }
                    
                    if (method.Name == LateUpdateName)
                    {
                        Debug.LogWarning(
                            GetWarningBaseText(
                                method.Name, "LateRun()", type.Name));
                    }
                }
            }
        }

        private string GetExceptionBaseText(string methodName, string className)
        {
            var classNameColored = GetColoredText(RedColor, className);
            var monoCacheNameColored = GetColoredText(OrangeColor, nameof(MonoCache));
            var methodNameColored = GetColoredText(RedColor, methodName);
            var baseTextColored = GetColoredText(WhiteColor,
                $"can't be implemented in subclass {classNameColored} of {monoCacheNameColored}. Use ");
            
            return $"{methodNameColored} {baseTextColored}";
        }

        private string GetWarningBaseText(string methodName, string recommendedMethod, string className)
        {
            var coloredClass = GetColoredText(OrangeColor, className);
            var monoCacheNameColored = GetColoredText(OrangeColor, nameof(MonoCache));
            var coloredMethod = GetColoredText(OrangeColor, methodName);
            
            var coloredRecommendedMethod =
                GetColoredText(BlueColor, "protected override void ") + 
                GetColoredText(OrangeColor, recommendedMethod);
            
            var coloredBaseText = GetColoredText(WhiteColor, 
                $"It is recommended to replace {coloredMethod} method with {coloredRecommendedMethod} " +
                $"in subclass {coloredClass} of {monoCacheNameColored}");
            
            return coloredBaseText;
        }

        private string GetColoredText(string color, string text)
        {
            if (color.IndexOf('#') == -1)
                color = '#' + color;

            return $"<color={color}>{text}</color>";
        }
    }
}