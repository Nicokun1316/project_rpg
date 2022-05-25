using System;
using System.Collections;
using System.Collections.Generic;
using Async;
using Items;
using Kyara;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class MikeRepellent : MonoBehaviour/*, Dialogue*/ {
    private Focusable focusable;
    private MovementController mike;
    private GameCharacter mikeChara;
    private bool scared;
    private bool dialogueFinished;
    private bool overcome;
    private CameraFX cam;
    [SerializeField] private GameObject drawer;
    [SerializeField] private UtilityItem courage;

    private void Awake() {
        var mikeObject = GameObject.FindWithTag("Player");
        mike = mikeObject.GetComponent<MovementController>();
        mikeChara = mikeObject.GetComponent<GameCharacter>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraFX>();
        
        focusable = GetComponent<Focusable>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Trigger entered!");
        StartCoroutine(ScareMike());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ScareMike() {
        GameManager.SetPhysicsEnabled(false);
        if (overcome) yield break;
        dialogueFinished = false;
        yield return new WaitForCharacterMoveDone(mike);

        Dictionary<String, String> options = null;

        yield return UIManager.INSTANCE.PerformDialogue(ChooseDialogue(), v => options = v);

        if (mikeChara.inventory.items.Contains(courage)) {
            if (options["mike_drink"] == "Yes") {
                yield return mike.MoveAsync(Vector2.up);
                mikeChara.inventory.items.Remove(courage);
                mikeChara.character.addExperience(10);
                overcome = true;
                yield return UIManager.INSTANCE.PerformDialogue(SimpleDialogue.Create(new DialogueChunk("",
                    "...\n|You down the whole bottle of whiskey.\n|Alcohol is not the way.")));
            } else {
                yield return mike.MoveAsync(Vector2.down);
            }
        } else {
            yield return mike.MoveAsync(Vector2.down);
        }

        if (!scared) {
            var reward = drawer.AddComponent<DialogueReward>();
            reward.item = courage;
            reward.inventory = mikeChara.inventory;
            yield return cam.Shake(0.5f, 1);
            yield return new WaitForSeconds(0.5f);
            yield return UIManager.INSTANCE.PerformDialogue(
                SimpleDialogue.Create(new DialogueChunk("", "You hear a loud bang coming from the south."))
            );
        }

        scared = true;
        GameManager.SetPhysicsEnabled(true);
    }

    private Dialogue ChooseDialogue() {
        List<DialogueChunk> dialogues = new List<DialogueChunk>() {
            new("", "..."),
            new("", "Do you want to receive courage?", "mike_drink", new List<string>(){"Yes", "No"})
        };
        var optional = MultilineDialogue.Create(dialogues);
        return (scared, mikeChara.inventory.items.Contains(courage)) switch {
            (false, _) => SimpleDialogue.Create(new DialogueChunk("", "...\n|You are too afraid to walk any further.")),
            (_, true) => optional,
            _ => SimpleDialogue.Create(new DialogueChunk("", "...\n|Have you found your missing courage yet?"))
        };
    }

    public void startDialogue() {
    }

    public DialogueChunk? current() {
        if (dialogueFinished) return null;
        return (scared, mikeChara.inventory.items.Contains(courage)) switch {
            (false, _) => new DialogueChunk("", "...\n|You are too afraid to walk any further."),
            (_, true) => new DialogueChunk("", "...\n|You down the whole bottle of whiskey.\n|Alcohol is not the answer."),
            _ => new DialogueChunk("", "...\n|Have you found your missing courage yet?")
        };
    }

    public void advance(string option = null) {
        dialogueFinished = true;
    }

    public void AddFinishedListener(Dialogue.OnDialogueFinished listener) { }
}
