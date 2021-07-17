using System.Linq;

namespace NTC.Global.System
{
    public class GlobalUpdate : SimpleSingleton<GlobalUpdate>
    {
        private void Update()
        {
            foreach (MonoCache monoCache in MonoCache.allTick.ToList())
            {
                monoCache.Tick();
            }
        }
        private void FixedUpdate()
        {
            foreach (MonoCache monoCache in MonoCache.allFixedTick.ToList())
            {
                monoCache.FixedTick();
            }
        }
        private void LateUpdate()
        {
            foreach (MonoCache monoCache in MonoCache.allLateTick.ToList())
            {
                monoCache.LateTick();
            }
        }
    }
}