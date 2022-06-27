using System;
using Cysharp.Threading.Tasks;

namespace Utils {
    public class LockableBoolean {
        public async UniTask<Lock> AcquireLock() {
            await UniTask.WaitWhile(() => isLocked);
            return new Lock(this);
        }
        private bool isLocked;
        public struct Lock : IDisposable {
            public Lock(LockableBoolean lockableBoolean) {
                b = lockableBoolean;
                b.isLocked = true;
            }
            private LockableBoolean b;
            public void Dispose() {
                b.isLocked = false;
            }
        }
    }
}
