using UnityEngine;

namespace Combat {
    public class CombatManager : Singleton {
        private static CombatManager _INSTANCE;

        public static CombatManager INSTANCE {
            get {
                if (_INSTANCE == null) {
                    var go = new GameObject("CombatManager");
                    _INSTANCE = go.AddComponent<CombatManager>();
                }

                return _INSTANCE;
            }
        }

        protected override Singleton instance { get => _INSTANCE; set => _INSTANCE = (CombatManager) value; }
        protected override void Initialize() {
            throw new System.NotImplementedException();
        }
    }
}
