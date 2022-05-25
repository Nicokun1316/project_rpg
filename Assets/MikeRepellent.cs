using System;
using System.Collections;
using System.Collections.Generic;
using Async;
using Items;
using Kyara;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class MikeRepellent : MonoBehaviour, Dialogue {
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
        if (overcome) yield break;
        dialogueFinished = false;
        yield return new WaitForCharacterMoveDone(mike);
        yield return UIManager.INSTANCE.PerformInteractionAsync(focusable);
        if (mikeChara.inventory.items.Contains(courage)) {
            yield return mike.MoveAsync(Vector2.up);
            mikeChara.inventory.items.Remove(courage);
            mikeChara.character.addExperience(10);
            overcome = true;
        } else {
            yield return mike.MoveAsync(Vector2.down);
        }

        if (!scared) {
            var reward = drawer.AddComponent<DialogueReward>();
            reward.item = courage;
            reward.inventory = mikeChara.inventory;
            yield return cam.Shake(0.5f, 1);
        }

        scared = true;
    }

    public void startDialogue() {
    }

    public DialogueChunk? current() {
        if (dialogueFinished) return null;
        if (!scared) {
            return new DialogueChunk("", "...\n|You are too afraid to walk any further.");
        } else if (mike.GetComponent<GameCharacter>().inventory.items.Contains(courage)) {
            return new DialogueChunk("", "...\n|You down the whole bottle of whiskey.\n|Alcohol is not the answer.");
        } else {
            return new DialogueChunk("", "...\n|Have you found your missing courage yet?");
        }
    }

    public void advance(string option = null) {
        dialogueFinished = true;
    }

    public void AddFinishedListener(Dialogue.OnDialogueFinished listener) { }
}
