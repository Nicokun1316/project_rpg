using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Skills;
using UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Cutscene.AbyssIntro {
    public class TeachHaru : MonoBehaviour {
        private bool taught = false;
        private MovementController mike;
        private Dialogue teachDialogue;
        [SerializeField] private Skill haru;

        private void Awake() {
            var mikeObject = GameObject.FindWithTag("Player");
            mike = mikeObject.GetComponent<MovementController>();
            teachDialogue = MultilineDialogue.Create(new List<DialogueChunk> {
                new("", "You cannot see anything.\n|Surrounded by the ever-darkness, you begin to remember who you are."),
                new("", "You learn <color=\"purple\">HARU</color>.\n|Access your skills through the Skills menu.")
            });
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Teach().Forget();
        }

        private async UniTask Teach() {
            if (taught) return;
            using var l = new PhysicsLock();
            await UniTask.WaitUntil(() => !mike.isMoving);
            await UIManager.INSTANCE.PerformDialogue(teachDialogue);
            mike.GetComponent<Light2D>().enabled = true;
            taught = true;
        }
    }
}
