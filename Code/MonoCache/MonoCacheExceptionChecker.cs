// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2022 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NTC.Global.Cache
{
    public class MonoCacheExceptionsChecker
    {
        private const BindingFlags MethodFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly;
        
        public void CheckForExceptions()
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
                    if (method.Name == GlobalUpdate.OnEnableMethodName)
                    {
                        Debug.LogException(new Exception(
                            $"{GetExceptionBaseText(GlobalUpdate.OnEnableMethodName, type.Name)}" +
                            $"{ColoredText.GetColoredText(ColoredText.BlueColor, "protected override void")} " +
                            $"{ColoredText.GetColoredText(ColoredText.OrangeColor, "OnEnabled()")}"));
                    }

                    if (method.Name == GlobalUpdate.OnDisableMethodName)
                    {
                        Debug.LogException(new Exception(
                            $"{GetExceptionBaseText(GlobalUpdate.OnDisableMethodName, type.Name)}" +
                            $"{ColoredText.GetColoredText(ColoredText.BlueColor, "protected override void")} " +
                            $"{ColoredText.GetColoredText(ColoredText.OrangeColor, "OnDisabled()")}"));
                    }
                    
                    if (method.Name == GlobalUpdate.UpdateMethodName)
                    {
                        Debug.LogWarning(
                            GetWarningBaseText(
                                method.Name, "Run()", type.Name));
                    }
                    
                    if (method.Name == GlobalUpdate.FixedUpdateMethodName)
                    {
                        Debug.LogWarning(
                            GetWarningBaseText(
                                method.Name, "FixedRun()", type.Name));
                    }
                    
                    if (method.Name == GlobalUpdate.LateUpdateMethodName)
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
            var classNameColored = ColoredText.GetColoredText(ColoredText.RedColor, className);
            var monoCacheNameColored = ColoredText.GetColoredText(ColoredText.OrangeColor, nameof(MonoCache));
            var methodNameColored = ColoredText.GetColoredText(ColoredText.RedColor, methodName);
            var baseTextColored = ColoredText.GetColoredText(ColoredText.WhiteColor,
                $"can't be implemented in subclass {classNameColored} of {monoCacheNameColored}. Use ");
            
            return $"{methodNameColored} {baseTextColored}";
        }

        private string GetWarningBaseText(string methodName, string recommendedMethod, string className)
        {
            var coloredClass = ColoredText.GetColoredText(ColoredText.OrangeColor, className);
            var monoCacheNameColored = ColoredText.GetColoredText(ColoredText.OrangeColor, nameof(MonoCache));
            var coloredMethod = ColoredText.GetColoredText(ColoredText.OrangeColor, methodName);
            
            var coloredRecommendedMethod =
                ColoredText.GetColoredText(ColoredText.BlueColor, "protected override void ") + 
                ColoredText.GetColoredText(ColoredText.OrangeColor, recommendedMethod);
            
            var coloredBaseText = ColoredText.GetColoredText(ColoredText.WhiteColor, 
                $"It is recommended to replace {coloredMethod} method with {coloredRecommendedMethod} " +
                $"in subclass {coloredClass} of {monoCacheNameColored}");
            
            return coloredBaseText;
        }
    }
}