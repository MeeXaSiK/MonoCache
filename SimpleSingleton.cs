namespace NTC.Global.System
{
    public class SimpleSingleton<X> : MonoCache where X : SimpleSingleton<X>
    {
        public static X instance { get; private set; }

        private void Awake()
        {
            instance = this as X;
            OnAwakened();
        }

        protected virtual void OnAwakened() { }
    }
}