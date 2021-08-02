using System.Collections.Generic;
using UnityEngine;

namespace NTC.Global.System
{
    public class MonoCache : MonoBehaviour
    {
        public static readonly List<MonoCache> AllTick = new List<MonoCache>(2000);
        public static readonly List<MonoCache> AllFixedTick = new List<MonoCache>(1000);
        public static readonly List<MonoCache> AllLateTick = new List<MonoCache>(1000);

        protected virtual void OnEnable() { AllTick.Add(this); OnEnabled(); }
        protected virtual void OnDisable() { AllTick.Remove(this); OnDisabled(); }
        
        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }

        public void Tick() => OnTick();
        public void FixedTick() => OnFixedTick();
        public void LateTick() => OnLateTick();

        protected virtual void OnTick() { }
        protected virtual void OnFixedTick() { }
        protected virtual void OnLateTick() { }
    }
}