// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2022 Night Train Code
// -------------------------------------------------------------------------------------------

using NTC.Global.System;

namespace NTC.Global.Cache
{
    public abstract class MonoCache : MonoAllocation
    {
        private void OnEnable()
        {
            OnEnabled();
            
            GlobalUpdate.
                GetNotNull().
                IfNotNull(globalUpdate =>
                {
                    globalUpdate.OnUpdate += Run;
                    globalUpdate.OnFixedUpdate += FixedRun;
                    globalUpdate.OnLateUpdate += LateRun;
                });
        }

        private void OnDisable()
        {
            GlobalUpdate.
                GetCanBeNull().
                IfNotNull(globalUpdate =>
                {
                    globalUpdate.OnUpdate -= Run;
                    globalUpdate.OnFixedUpdate -= FixedRun;
                    globalUpdate.OnLateUpdate -= LateRun;
                });
            
            OnDisabled();
        }

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        
        protected virtual void Run() { }
        protected virtual void FixedRun() { }
        protected virtual void LateRun() { }
    }
}
