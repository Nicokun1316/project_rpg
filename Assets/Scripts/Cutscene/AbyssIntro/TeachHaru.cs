using System;
using System.Collections.Generic;
using System.Linq;
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
                new("", $"You learn {haru.Rep()}.\n|Access your skills through the {MenuEnum.Skills.Rep()} menu.")
            };
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Teach().Forget();
        }

        private async UniTask Teach() {
            if (taught) return;
            var delay = TimeSpan.FromMilliseconds(1500);
            using var l = GameManager.AcquirePhysicsLock();
            await UniTask.WaitUntil(() => !mike.IsMoving);
            using var s = new SafeStatLock<float>(mike.Speed, 1.5f, v => mike.Speed = v);
            
            for (int i = 0; i < 4; ++i) {
                await mike.MoveCharacter(Vector2.up);
            }

            await UniTask.Delay(TimeSpan.FromMilliseconds(1500));
            foreach (var orientation in new List<Orientation> {Orientation.Right, Orientation.Down, Orientation.Left, Orientation.Up}) {
                mike.Turn(orientation);
                await UniTask.Delay(delay);
            }
            await UIManager.INSTANCE.PerformDialogue(teachDialogue);
            mikeChara.skills.knownSkills.Add(haru);
            mike.GetComponent<Light2D>().enabled = true;
            taught = true;
        }
    }
}
