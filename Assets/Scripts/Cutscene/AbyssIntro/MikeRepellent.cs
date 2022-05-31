using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Items;
using Kyara;
using UI;
using UnityEngine;
using Utils;

public class MikeRepellent : MonoBehaviour {
    private Focusable focusable;
    private MovementController mike;
    private GameCharacter mikeChara;
    private bool scared;
    private bool overcome;
    private CameraFX mainCamera;
    [SerializeField] private GameObject drawer;
    [SerializeField] private GameObject drunkEffect;
    [SerializeField] private UtilityItem courage;

    private void Awake() {
        var mikeObject = GameObject.FindWithTag("Player");
        mike = mikeObject.GetComponent<MovementController>();
        mikeChara = mikeObject.GetComponent<GameCharacter>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFX>();
        
        focusable = GetComponent<Focusable>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Trigger entered!");
        //StartCoroutine(ScareMike());
        ScareMichael().Forget();
    }

    private async UniTask ScareMichael() {
        GameManager.SetPhysicsEnabled(false);
        if (overcome) return;
        await UniTask.WaitUntil(() => !mike.isMoving);
        Dictionary<String, String> options = await UIManager.INSTANCE.PerformDialogue(ChooseDialogue());
        
        if (mikeChara.inventory.items.Contains(courage)) {
            if (options.GetOrDefault("mike_drink", "No") == "Yes") {
                await mike.MoveCharacter(Vector2.up);
                mikeChara.inventory.items.Remove(courage);
                mikeChara.character.addExperience(10);
                overcome = true;
                await UIManager.INSTANCE.PerformDialogue(SimpleDialogue.Create(new DialogueChunk("",
                    "...\n|You down the whole bottle of whiskey.\n|Alcohol is not the way.")));
                drunkEffect.SetActive(true);
            } else {
                await mike.MoveCharacter(Vector2.down);
            }
        } else {
            await mike.MoveCharacter(Vector2.down);
        }

        if (!scared) {
            var reward = drawer.AddComponent<DialogueReward>();
            reward.item = courage;
            reward.inventory = mikeChara.inventory;
            await mainCamera.Shake(0.5f, 1);
            await UniTask.Delay(TimeSpan.FromMilliseconds(500));
            await UIManager.INSTANCE.PerformDialogue(
                SimpleDialogue.Create(new DialogueChunk("", "You hear a loud bang coming from the south."))
            );
        }

        scared = true;
        GameManager.SetPhysicsEnabled(true);
    }

    private Dialogue ChooseDialogue() {
        List<DialogueChunk> dialogues = new List<DialogueChunk>() {
            new("", "..."),
            new("", "Do you want to receive courage?", "mike_drink", new List<string> {"Yes", "No"})
        };
        var optional = MultilineDialogue.Create(dialogues);
        return (scared, mikeChara.inventory.items.Contains(courage)) switch {
            (false, _) => SimpleDialogue.Create(new DialogueChunk("", "...\n|You are too afraid to walk any further.")),
            (_, true) => optional,
            _ => SimpleDialogue.Create(new DialogueChunk("", "...\n|Have you found your missing courage yet?"))
        };
    }
}
