using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Items;
using Kyara;
using UI;
using UI.Dialogue;
using UnityEngine;
using Utils;

namespace Cutscene.AbyssIntro {
    public class MikeRepellent : MonoBehaviour {
        private MovementController mike;
        private GameCharacter mikeChara;
        private bool scared;
        private bool overcome;
        private CameraFX mainCamera;
        private List<DialogueChunk> alcoholWarnings;
        [SerializeField] private GameObject drawer;
        [SerializeField] private GameObject drunkEffect;
        [SerializeField] private UtilityItem courage;
        [SerializeField] private FrightfulDisappearance shadowMike;
        [SerializeField] private AudioClip shadowClip;

        private void Awake() {
            var mikeObject = GameObject.FindWithTag("Player");
            mike = mikeObject.GetComponent<MovementController>();
            mikeChara = mikeObject.GetComponent<GameCharacter>();
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFX>();
            alcoholWarnings = new List<DialogueChunk> {
                new("", $"You down the whole {courage.Rep()}."),
                new("", "Short term effects of drinking alcohol include:\n|*Changes in mood\n|*Lowered inhibitions"),
                new("", "*Impulsive behaviour\n|*Slowed or slurred speech\n|*Explosive diarrhoea"),
                new("",
                    "*Trouble making love decisions\n|*Pregnancy caused by bad decisions\n|*Blackouts, including electricity blackouts."),
                new("",
                    "Long term effects of alcohol include:\n|*Changes in weight (beer belly)\n|*Homelessness"),
                new("",
                    "*Homelessness-induced insomnia\n|*Frequent visits at sobering-up cells\n|*Rage-induced death"),
                new("", "Please be mindful that underage drinking is illegal in all 49 states.")
            };
        }

        private void OnTriggerEnter2D(Collider2D col) {
            ScareMichael().Forget();
        }

        private async UniTask ScareMichael() {
            using var l = GameManager.AcquirePhysicsLock();
            if (overcome) return;
            await UniTask.WaitUntil(() => !mike.IsMoving);
            Dictionary<String, String> options = await UIManager.INSTANCE.PerformDialogue(ChooseDialogue());

            if (mikeChara.inventory.HasItem(courage)) {
                if (options.GetOrDefault("mike_drink", "No") == "Yes") {
                    await DrinkCourage();
                } else {
                    await mike.MoveCharacter(Vector2.down, Orientation.Up);
                }
            } else {
                await mike.MoveCharacter(Vector2.down, Orientation.Up);
            }

            if (!scared) {
                AddCourageToDrawer();
                await Bang();
            }

            scared = true;
        }

        private async UniTask Bang() {
            var delay = TimeSpan.FromMilliseconds(1000);
            await mainCamera.Shake(0.5f, 1);
            await UniTask.Delay(delay);

            foreach (var orientation in new List<Orientation> {Orientation.Right, Orientation.Down, Orientation.Left, Orientation.Down}) {
                mike.Turn(orientation);
                await UniTask.Delay(delay);
            }
            
            /*mike.Turn(Orientation.Right);
            await UniTask.Delay(delay);
            mike.Turn(Orientation.Down);
            await UniTask.Delay(delay);
            mike.Turn(Orientation.Left);
            await UniTask.Delay(delay);
            mike.Turn(Orientation.Down);
            await UniTask.Delay(delay);*/
            await UIManager.INSTANCE.PerformDialogue(new DialogueChunk("",
                "You hear a loud bang coming from the south.        \nPerhaps it's worth checking out?"));
        }


        private async UniTask DrinkCourage() {
            await mike.MoveCharacter(Vector2.up);
            mikeChara.inventory.RemoveItem(courage);
            mikeChara.character.AddExperience(10);
            overcome = true;
            await UIManager.INSTANCE.PerformDialogue(alcoholWarnings);
            drunkEffect.SetActive(true);
            alcoholWarnings = null;
        }

        private Dialogue ChooseDialogue() {
            List<DialogueChunk> dialogues = new List<DialogueChunk>() {
                new("", "..."),
                new("", "Do you want to receive courage?", "mike_drink", new List<string> {"Yes", "No"})
            };
            return (scared, mikeChara.inventory.HasItem(courage)) switch {
                (false, _) => new SimpleDialogue(new DialogueChunk("",
                    "...\n|You are too afraid to walk any further.")),
                (_, true) => new MultilineDialogue(dialogues),
                _ => new SimpleDialogue(new DialogueChunk("", "...\n|Have you found your missing courage yet?"))
            };
        }

        private void AddCourageToDrawer() {
            Destroy(drawer.GetComponent<Dialogue>() as MonoBehaviour);
            var md = drawer.AddComponent<MultilineDialogueComponent>();
            md.Initialize(new List<DialogueChunk> {
                new("",
                    $"Inconspicuously, a new object has appeared in the drawer.\n|You receive {courage.Rep()}.\n|Access your inventory through the {MenuEnum.Items.Rep()} menu.")
            });

            md.dialogue.AddStartedListener(() => {
                AudioManager.INSTANCE.PlayMusic(shadowClip);
            });

            md.dialogue.AddFinishedListener(() => {
                shadowMike.Show();
                mikeChara.inventory.AddItem(courage);
                Destroy(md);
                drawer.AddComponent<MultilineDialogueComponent>().Initialize(new List<DialogueChunk> {
                    new("", "Nothing to see here anymore.")
                });
                ;
            });
        }
    }
}
