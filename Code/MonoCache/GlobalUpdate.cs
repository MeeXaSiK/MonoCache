// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2022 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
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

        public const string OnEnableMethodName = "OnEnable";
        public const string OnDisableMethodName = "OnDisable";
        
        public const string UpdateMethodName = nameof(Update);
        public const string FixedUpdateMethodName = nameof(FixedUpdate);
        public const string LateUpdateMethodName = nameof(LateUpdate);

        private readonly MonoCacheExceptionsChecker monoCacheExceptionsChecker = 
            new MonoCacheExceptionsChecker();
        
        private void Awake()
        {
            monoCacheExceptionsChecker.CheckForExceptions();
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
    }
}