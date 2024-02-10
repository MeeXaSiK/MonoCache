// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2024 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace NTC.MonoCache
{
    public abstract class MonoCache : MonoBehaviour
    {
        internal int _index;

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
                _globalUpdate.Add(this);
            }
        }

        private void OnDisable()
        {
            if (_isSetup)
            {
                _globalUpdate.Remove(this);
            }

            OnDisabled();
        }

        private void TrySetup()
        {
            if (Application.isPlaying)
            {
                _globalUpdate = GlobalUpdate.Instance;
                _isSetup = true;
            }
            else
            {
                throw new Exception(
                    $"You tries to get {nameof(GlobalUpdate)} instance when application is not playing!");
            }
        }

        internal void RaiseRun() => Run();
        internal void RaiseFixedRun() => FixedRun();
        internal void RaiseLateRun() => LateRun();

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        protected virtual void Run() { }
        protected virtual void FixedRun() { }
        protected virtual void LateRun() { }
    }
}
