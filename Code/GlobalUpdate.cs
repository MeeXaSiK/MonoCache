// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021-2024 Night Train Code
// -------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NTC.MonoCache
{
    [DisallowMultipleComponent]
    public sealed class GlobalUpdate : MonoBehaviour
    {
        private static GlobalUpdate s_instance;
        private readonly List<MonoCache> _monoCaches = new List<MonoCache>(32);
        private int _count;

        internal static GlobalUpdate Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<GlobalUpdate>();
#if DEBUG
                    if (s_instance == null)
                    {
                        Debug.LogError($"The <{nameof(GlobalUpdate)}> was not found on the scene!");
                    }
#endif
                }

                return s_instance;
            }
        }

#if DEBUG
        private void Awake()
        {
            MonoCacheExceptionsDetector.CheckForExceptions();
        }
#endif
        private void Update()
        {
            for (int i = 0; i < _count; i++)
            {
                _monoCaches[i].RaiseRun();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _count; i++)
            {
                _monoCaches[i].RaiseFixedRun();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _count; i++)
            {
                _monoCaches[i].RaiseLateRun();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Add(MonoCache monoCache)
        {
            _monoCaches.Add(monoCache);
            monoCache._index = _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Remove(MonoCache monoCache)
        {
            var lastComponent = _monoCaches[_count-1];
            _monoCaches[monoCache._index] = lastComponent;
            lastComponent._index = monoCache._index;
            _monoCaches.RemoveAt(--_count);
        }
    }
}