using Items;
using UnityEngine;
using Utils;

namespace UI.Dialogue {
    public class ChestDialogue : MonoBehaviour, ForwardDialogue {
        private Dialogue dial;
        private static Dialogue alreadyGottened = new SimpleDialogue(new DialogueChunk(null, "Nothing to see here anymore."));

        public Dialogue dialogue {
            get {
                var reward = GetComponent<DialogueReward>();
                dial ??= new SimpleDialogue(new DialogueChunk(null, $"You receive {reward.Item.Rep()} x1!"));

                return reward.Granted ? alreadyGottened : dial;
            }
        }
    }
}
