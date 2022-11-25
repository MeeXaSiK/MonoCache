// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2022 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
using NTC.Global.Cache.Interfaces;
using NTC.Global.System;
using UnityEngine.Device;

namespace NTC.Global.Cache
{
    public abstract class MonoCache : MonoAllocation, IRunSystem, IFixedRunSystem, ILateRunSystem
    {
        private GlobalUpdate _globalUpdate;
        private bool _isSetup;
        
        private void OnEnable()
        {
            OnEnabled();

            if (_isSetup == false)
            {
                TrySetup();
            }

            if (_isSetup)
            {
                SubscribeToGlobalUpdate();
            }
        }

        private void OnDisable()
        {
            if (_isSetup)
            {
                UnsubscribeFromGlobalUpdate();
            }

            OnDisabled();
        }

        private void TrySetup()
        {
            if (Application.isPlaying)
            {
                _globalUpdate = Singleton<GlobalUpdate>.Instance;
                _isSetup = true;
            }
            else
            {
                throw new Exception(
                    $"You tries to get {nameof(GlobalUpdate)} instance when application is not playing!");
            }
        }
        
        private void SubscribeToGlobalUpdate()
        {
            _globalUpdate.RunSystems.Add(this);
            _globalUpdate.FixedRunSystems.Add(this);
            _globalUpdate.LateRunSystems.Add(this);
        }

        private void UnsubscribeFromGlobalUpdate()
        {
            _globalUpdate.RunSystems.Remove(this);
            _globalUpdate.FixedRunSystems.Remove(this);
            _globalUpdate.LateRunSystems.Remove(this);
        }

        void IRunSystem.OnRun() => Run();
        void IFixedRunSystem.OnFixedRun() => FixedRun();
        void ILateRunSystem.OnLateRun() => LateRun();

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        
        protected virtual void Run() { }
        protected virtual void FixedRun() { }
        protected virtual void LateRun() { }
    }
}
