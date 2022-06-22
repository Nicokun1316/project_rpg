using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UI;
using UI.Dialogue;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TorchPuzzle : MonoBehaviour {
    [SerializeField] private Vector2Int unlockPosition;
    [SerializeField] private Vector2Int unlockSpan;
    private TileHotSwapScope sun, fire, ice;
    // Start is called before the first frame update
    public bool Complete { get; private set; } = false;

    void Start() {
        sun = transform.Find("Sun").GetComponent<TileHotSwapScope>();
        fire = transform.Find("Fire").GetComponent<TileHotSwapScope>();
        ice = transform.Find("Ice").GetComponent<TileHotSwapScope>();
    }

    public void RecalculateTorches() {
        if (Complete) return;
        var sunLit = sun.GetComponentsInChildren<LightableLantern>().Count(t => t.IsLit);
        var fireLit = fire.GetComponentsInChildren<LightableLantern>().Count(t => t.IsLit);
        var iceLit = ice.GetComponentsInChildren<LightableLantern>().Count(t => t.IsLit);
        if (sunLit + iceLit + fireLit >= 6) {
            if (sunLit == 6) {
                Complete = true;
                foreach (var l in GetComponentsInChildren<LightableLantern>()) {
                    l.IsLit = true;
                }
                UIManager.INSTANCE.PerformDialogue(new DialogueChunk(null, "A new path waits ahead.")).Forget();
                var map = GameObject.FindWithTag("ObstaclesBase").GetComponent<Tilemap>();
                for (var y = unlockPosition.y; y < unlockPosition.y + unlockSpan.y; ++y) {
                    for (var x = unlockPosition.x; x < unlockPosition.x + unlockSpan.x; ++x) {
                        var v = new Vector3Int(x, y);
                        print($"Setting {v} to null");
                        map.SetTile(new Vector3Int(x, y), null);
                    }
                }
            } else {
                foreach (var l in GetComponentsInChildren<LightableLantern>()) {
                    l.IsLit = false;
                }
                UIManager.INSTANCE.PerformDialogue(new DialogueChunk(null, "Your strength wanes.")).Forget();
            }
        }
    }
}
