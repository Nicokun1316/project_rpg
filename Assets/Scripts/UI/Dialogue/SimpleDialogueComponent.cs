using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Dialogue {
    public class SimpleDialogueComponent : MonoBehaviour, ForwardDialogue {
        [SerializeField] private SimpleDialogue dial;
        public Dialogue dialogue => dial;

        public void Initialize(DialogueChunk dialogue) {
            dial = new SimpleDialogue(dialogue);
        }
    }
}
