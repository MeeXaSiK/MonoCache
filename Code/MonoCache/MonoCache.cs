// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2022 Night Train Code
// -------------------------------------------------------------------------------------------

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
                Setup();
            }

            _globalUpdate.RunSystems.Add(this);
            _globalUpdate.FixedRunSystems.Add(this);
            _globalUpdate.LateRunSystems.Add(this);
        }

        private void OnDisable()
        {
            _globalUpdate.RunSystems.Remove(this);
            _globalUpdate.FixedRunSystems.Remove(this);
            _globalUpdate.LateRunSystems.Remove(this);

            OnDisabled();
        }

        private void Setup()
        {
            if (Application.isPlaying)
            {
                _globalUpdate = Singleton<GlobalUpdate>.Instance;
            }

            _isSetup = true;
        }

        private void IRunSystem.OnRun() => Run();
        private void IFixedRunSystem.OnFixedRun() => FixedRun();
        private void ILateRunSystem.OnLateRun() => LateRun();

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }

        protected virtual void Run() { }
        protected virtual void FixedRun() { }
        protected virtual void LateRun() { }
    }
}