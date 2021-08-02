using UnityEngine;

namespace NTC.Global.System
{
    public class GlobalUpdate : MonoBehaviour
    {
        private void Update()
        {
            for (int i = 0; i < MonoCache.AllTick.Count; i++)
            {
                MonoCache.AllTick[i].Tick();
            }
        }
        private void FixedUpdate()
        {
            for (int i = 0; i < MonoCache.AllFixedTick.Count; i++)
            {
                MonoCache.AllFixedTick[i].FixedTick();
            }
        }
        private void LateUpdate()
        {
            for (int i = 0; i < MonoCache.AllLateTick.Count; i++)
            {
                MonoCache.AllLateTick[i].LateTick();
            }
        }
    }
}