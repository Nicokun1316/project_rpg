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

        private void Awake() {
            var mikeObject = GameObject.FindWithTag("Player");
            mike = mikeObject.GetComponent<MovementController>();
            mikeChara = mikeObject.GetComponent<GameCharacter>();
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFX>();
            alcoholWarnings = new List<DialogueChunk> {
                new("", $"You down the whole <{T.Color(GColor.Item)}>Courage</color>."),
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
            using var l = new PhysicsLock();
            if (overcome) return;
            await UniTask.WaitUntil(() => !mike.isMoving);
            Dictionary<String, String> options = await UIManager.INSTANCE.PerformDialogue(ChooseDialogue());

            if (mikeChara.inventory.items.Contains(courage)) {
                if (options.GetOrDefault("mike_drink", "No") == "Yes") {
                    await DrinkCourage();
                } else {
                    await mike.MoveCharacter(Vector2.down);
                }
            } else {
                await mike.MoveCharacter(Vector2.down);
            }

            if (!scared) {
                AddCourageToDrawer();
                await mainCamera.Shake(0.5f, 1);
                await UniTask.Delay(TimeSpan.FromMilliseconds(500));
                await UIManager.INSTANCE.PerformDialogue(new DialogueChunk("",
                    "You hear a loud bang coming from the south.        \nPerhaps its worth checking out?"));
            }

            scared = true;
        }


        private async UniTask DrinkCourage() {
            await mike.MoveCharacter(Vector2.up);
            mikeChara.inventory.items.Remove(courage);
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
            return (scared, mikeChara.inventory.items.Contains(courage)) switch {
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
                    $"Inconspicuously, a new object has appeared in the drawer.\n|You receive <{T.Color(GColor.Item)}>COURAGE</color>.\n|Access your inventory through the <{T.Color(GColor.MenuRef)}>Items</color> menu.")
            });

            md.dialogue.AddFinishedListener(() => {
                shadowMike.Show();
                mikeChara.inventory.items.Add(courage);
                Destroy(md);
                drawer.AddComponent<MultilineDialogueComponent>().Initialize(new List<DialogueChunk> {
                    new("", "Nothing to see here anymore.")
                });
                ;
            });
        }
    }
}
