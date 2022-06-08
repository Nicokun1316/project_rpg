using System;

namespace Cutscene {
    public class SafeStatLock<T> : IDisposable {
        public SafeStatLock(T stat, T newValue, Action<T> setter) {
            originalValue = stat;
            this.setter = setter;
            setter(newValue);
        }

        private readonly Action<T> setter;
        private readonly T originalValue;
        public void Dispose() {
            setter(originalValue);
        }
    }
}
