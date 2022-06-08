using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Dialogue {
    public class MultilineDialogueComponent : MonoBehaviour, ForwardDialogue {
        [SerializeField] private MultilineDialogue dial;
        public Dialogue dialogue => dial;

        public void Initialize(List<DialogueChunk> dialogues) {
            dial = new MultilineDialogue(dialogues);
        }

    }
}
