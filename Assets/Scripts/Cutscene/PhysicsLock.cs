using System;

namespace Cutscene {
    public class PhysicsLock : IDisposable {
        public PhysicsLock() {
            GameManager.SetPhysicsEnabled(false);
        }
        public void Dispose() {
            GameManager.SetPhysicsEnabled(true);
        }
    }
}
