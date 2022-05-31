using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class AbyssIntro : MonoBehaviour {
    [SerializeField] private GameObject player;

    [SerializeField] private Tilemap bedTilemap;
    [SerializeField] private Vector3Int bedPosition;
    [SerializeField] private List<TileBase> bedTiles;

    private void Start() {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Light2D>().enabled = false;
        var im = InputManager.INSTANCE;
        im.RegisterInputHook("Move", _ => {
            player.GetComponent<SpriteRenderer>().enabled = true;
            im.UnregisterInputHook("Confirm");
            im.UnregisterInputHook("Cancel");
            for (var i = bedPosition; i.x - bedPosition.x < bedTiles.Count; i += Vector3Int.right) {
                var tile = bedTiles[i.x - bedPosition.x];
                bedTilemap.SetTile(i, tile);
            }
            GameManager.SetPhysicsEnabled(false);
            LearnHaru().Forget();

            return true;
        });
        im.RegisterInputHook("Confirm", _ => false);
        im.RegisterInputHook("Cancel", _ => false);
    }

    private async UniTask LearnHaru() {
        var dialogues = new List<DialogueChunk> {
            new(null, "You are surrounded by pitch black.\n|Amidst the darkness, you begin to remember who you are."),
            new(null, "You learn <color=\"purple\">HARU</color>.\n|Access your skills through the Skills menu.")
        };
        await player.GetComponent<MovementController>().MoveCharacter(Vector2.down);
        await UIManager.INSTANCE.PerformDialogue(MultilineDialogue.Create(dialogues));
        player.GetComponent<Light2D>().enabled = true;
        GameManager.SetPhysicsEnabled(true);
    }
}
