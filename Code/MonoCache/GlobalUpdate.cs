// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2022 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NTC.Global.Cache.Interfaces;
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

        public readonly List<IRunSystem> RunSystems = new List<IRunSystem>(1024);
        public readonly List<IFixedRunSystem> FixedRunSystems = new List<IFixedRunSystem>(512);
        public readonly List<ILateRunSystem> LateRunSystems = new List<ILateRunSystem>(256);

        private readonly MonoCacheExceptionsChecker monoCacheExceptionsChecker = 
            new MonoCacheExceptionsChecker();
        
        private void Awake()
        {
            monoCacheExceptionsChecker.CheckForExceptions();
        }

        private void Update()
        {
            for (int i = 0; i < RunSystems.Count; i++)
            {
                RunSystems[i].OnRun();
            }
            
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < FixedRunSystems.Count; i++)
            {
                FixedRunSystems[i].OnFixedRun();
            }

            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            for (int i = 0; i < LateRunSystems.Count; i++)
            {
                LateRunSystems[i].OnLateRun();
            }

            OnLateUpdate?.Invoke();
        }
    }
}