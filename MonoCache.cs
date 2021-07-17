using System.Collections.Generic;
using UnityEngine;

namespace NTC.Global.System
{
    public class MonoCache : MonoBehaviour
    {
        public static HashSet<MonoCache> allTick = new HashSet<MonoCache>();
        public static HashSet<MonoCache> allFixedTick = new HashSet<MonoCache>();
        public static HashSet<MonoCache> allLateTick = new HashSet<MonoCache>();

        protected virtual void OnEnable() { allTick.Add(this); OnEnabled(); }
        protected virtual void OnDisable() { allTick.Remove(this); OnDisabled(); }
        
        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }

        public void Tick() => OnTick();
        public void FixedTick() => OnFixedTick();
        public void LateTick() => OnLateTick();

        public virtual void OnTick() { }
        public virtual void OnFixedTick() { }
        public virtual void OnLateTick() { }
    }
}