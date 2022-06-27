using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class TriggerTheScarySpookyStuff : MonoBehaviour {
    private bool hasBeenRun = false;
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            if (hasBeenRun) return;
            hasBeenRun = true;
            DoTheSpook().Forget();
        }
    }

    private async UniTask DoTheSpook() {
        var movementControllers = gameObject.parent().GetComponentsInChildren<MovementController>(true);
        var tentacles = movementControllers.Where(it => it.name == "Tentacle").ToArray();
        var shadowMike = movementControllers.First(it => it.name == "MrShadow");
        var astro = movementControllers.First(it => it.name == "Astro");
        var mike = GameObject.FindWithTag("Player").GetComponent<MovementController>();

        shadowMike.gameObject.SetActive(true);
        astro.gameObject.SetActive(true);
        foreach (var tentacle in tentacles) {
            tentacle.gameObject.SetActive(true);
        }

        await UniTask.WaitUntil(() => !mike.IsMoving);
        using var pl = GameManager.AcquirePhysicsLock();
        using var turnSeq = new List<Orientation> {Orientation.Right, Orientation.Down, Orientation.Left}.GetEnumerator();
        await UniTask.NextFrame();
        for (int i = 0; i < 7; ++i) {
            foreach (var tentacle in tentacles) {
                var p = mike.Position - tentacle.Position;
                tentacle.MoveCharacter((mike.Position - tentacle.Position).normalized).Forget();
            }

            if (i % 2 == 0 && i != 0) {
                turnSeq.MoveNext();
                await mike.MoveCharacter(Vector2.zero, turnSeq.Current);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(2));
        }

        await shadowMike.MoveCharacter((mike.Position - shadowMike.Position).normalized);
        await UniTask.Delay(1000);

        for (int i = 0; i < 8; ++i) {
            await shadowMike.MoveCharacter((mike.Position - shadowMike.Position).normalized);
            if (i == 4) {
                print($"turning himst up at {i}");
                await mike.MoveCharacter(Vector2.zero, Orientation.Up);
            }
        }

        await UniTask.Delay(500);

        await mike.MoveCharacter(Vector2.down, Orientation.Up);

        await UniTask.Delay(1000);
        await shadowMike.MoveCharacter(Vector2.zero, Orientation.Right);
        for (int i = 0; i < 6; ++i) {
            await astro.MoveCharacter(Vector2.left);
            if (i % 2 == 0) {
                await mike.MoveCharacter(Vector2.zero, Orientation.Right);
            }
        }

        var lumin = astro.GetComponent<BouncyLuminosity>();
        await UniTask.Delay(1000);
        await lumin.AwaitCancellation();
        await lumin.Blink(6, 1, 10, 1, 2f);
        var stopAudioTask = AudioManager.INSTANCE.StopPlaying();
        var lightenUpTask = lumin.LightenUp(30, 1, 80, 1, 2f);
        await UniTask.WhenAll(stopAudioTask, lightenUpTask);
        SceneManager.LoadScene("Scenes/Credits");
    }
}
