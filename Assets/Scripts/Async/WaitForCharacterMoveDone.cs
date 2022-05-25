using UnityEngine;

namespace Async {
    public class WaitForCharacterMoveDone : CustomYieldInstruction {
        private MovementController player;
        private bool shouldWait = true;
        
        public WaitForCharacterMoveDone(MovementController player) {
            this.player = player;
            player.OnMoveFinished += CharacterMoveFinished;
        }

        public override bool keepWaiting {
            get {
                if (!shouldWait) {
                    shouldWait = true;
                    player.OnMoveFinished -= CharacterMoveFinished;
                    return false;
                }
                return true;
            }
        }
        
        void CharacterMoveFinished() {
            shouldWait = false;
        }
    }
}
