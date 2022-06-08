using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kyara;
using Skills;
using UI;
using UI.Dialogue;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utils;

namespace Cutscene.AbyssIntro {
    public class TeachHaru : MonoBehaviour {
        private bool taught = false;
        private MovementController mike;
        private GameCharacter mikeChara;
        private List<DialogueChunk> teachDialogue;
        [SerializeField] private Skill haru;

        private void Awake() {
            var mikeObject = GameObject.FindWithTag("Player");
            mike = mikeObject.GetComponent<MovementController>();
            mikeChara = mikeObject.GetComponent<GameCharacter>();
            teachDialogue = new List<DialogueChunk> {
                new("", "You cannot see anything.\n|Surrounded by the ever-darkness, you begin to remember who you are."),
                new("", $"You learn <{T.Color(GColor.Skill)}>HARU</color>.\n|Access your skills through the <{T.Color(GColor.MenuRef)}>Skills</color> menu.")
            };
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Teach().Forget();
        }

        private async UniTask Teach() {
            if (taught) return;
            using var l = new PhysicsLock();
            await UniTask.WaitUntil(() => !mike.isMoving);
            await UIManager.INSTANCE.PerformDialogue(teachDialogue);
            mikeChara.skills.knownSkills.Add(haru);
            mike.GetComponent<Light2D>().enabled = true;
            taught = true;
        }
    }
}
