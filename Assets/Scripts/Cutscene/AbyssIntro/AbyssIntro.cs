using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

namespace Cutscene.AbyssIntro {
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
            
                LeaveBed().Forget();

                return true;
            });
            im.RegisterInputHook("Confirm", _ => false);
            im.RegisterInputHook("Cancel", _ => false);
        }

        private async UniTask LeaveBed() {
            using var l = new PhysicsLock();
            await player.GetComponent<MovementController>().MoveCharacter(Vector2.down);
        }
    }
}
