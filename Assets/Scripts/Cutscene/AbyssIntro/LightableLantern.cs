using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Skills;
using UI;
using UI.Dialogue;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using Utils;
using Color = UnityEngine.Color;

public class LightableLantern : MonoBehaviour, InteractionTarget {
    private Skill activateSkill;
    private bool isLit = false;
    private Tilemap interactibles;
    private Dialogue lightDialogue;
    private Dialogue extinguishDialogue;
    private new readonly String tag = "Use";
    private readonly List<String> options = new() {"Yes", "No"};
    private TileHotSwapScope scope;
    private new Light2D light;
    private TorchPuzzle puzzle;

    public bool IsLit {
        get => isLit;
        set {
            if (value != isLit) {
                var position = Vector3Int.FloorToInt(transform.position);
                var ntiles = value ? scope.SwapTiles : scope.BaseTiles;
                for (var y = position.y; y < position.y + ntiles.Count; ++y) {
                    interactibles.SetTile(new Vector3Int(position.x, y), ntiles[y - position.y]);
                }

                if (value) {
                    var go = new GameObject("Light") {
                        transform = {
                            parent = transform
                        }
                    };
                    light = go.AddComponent<Light2D>();
                    light.intensity = 1;
                    light.pointLightOuterRadius = 2.0f;
                    light.color = Color.red;
                    light.transform.localPosition = new Vector3(1.0f / 32.0f, 0.7f);
                } else {
                    Destroy(light.gameObject);
                    light = null;
                }

                isLit = value;
                puzzle.RecalculateTorches();
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        scope = GetComponentInParent<TileHotSwapScope>();
        interactibles = scope.Interactibles;
        activateSkill = scope.ActivateSkill;
        lightDialogue =
            new MultilineDialogue(new List<DialogueChunk> {
                new(null, "An unlit torch."),
                new(null, $"Do you want to use {activateSkill.Rep()}?", tag, options)
            });
        extinguishDialogue =
            new MultilineDialogue(new List<DialogueChunk> {
                new (null, "A lit torch."),
                new(null, $"Do you want to undo {activateSkill.Rep()}?", tag, options)
            });
        puzzle = GetComponentInParent<TorchPuzzle>();
    }

    public void Interact() {
        if (!puzzle.Complete) {
            TryLight().Forget();
        }
    }

    public async UniTask TryLight() {
        if (IsLit) {
            var shouldntLight = (await UIManager.INSTANCE.PerformDialogue(extinguishDialogue)).GetOrDefault(tag, "No") ==
                                "Yes";
            IsLit = !shouldntLight;
        } else {
            var shouldLight = (await UIManager.INSTANCE.PerformDialogue(lightDialogue)).GetOrDefault(tag, "No") ==
                              "Yes";
            IsLit = shouldLight;
        }
    }
}
