using System;
using System.Collections.Generic;
using System.Linq;
using Cutscene;
using Cysharp.Threading.Tasks;
using FX;
using Lore;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GameManager : Singleton {
    private InputController playerController;
    private GameObject player;
    private Rigidbody2D playerBody;
    private ContactFilter2D findFilter;
    private List<RaycastHit2D> raycastResults = new();
    private int physicsLocks;
    public GameState currentGameState { get; private set; }

    public static GameManager INSTANCE { get; private set; }

    public static readonly int PPU = 32;
    
    public GameManager() {
        currentGameState = GameState.WORLD;
        findFilter.useLayerMask = true;
    }

    public static void SetResolution(String resolutionString) {
        var monitor = Screen.mainWindowDisplayInfo;

        var (w, h, fs) = resolutionString switch {
            "2" => (1280, 960, false),
            "3" => (1920, 1440, false),
            "fullscreen" => (monitor.width, monitor.height, true),
            _ => (640, 480, false)
        };
        
        Screen.SetResolution(w, h, fs);
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void InitializeGame() {
        SetResolution(PlayerPrefs.GetString("Resolution", "2"));
        
    }

    public static Vector2 PixelClamp(Vector2 vector) {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(vector.x * PPU),
            Mathf.RoundToInt(vector.y * PPU));
        return vectorInPixels / PPU;
    }

    protected override void Initialize() {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<InputController>();
        playerBody = player.GetComponent<Rigidbody2D>();
        physicsLocks = 0;
    }

    protected override Singleton instance {
        get => INSTANCE;
        set => INSTANCE = (GameManager) value;
    }


    public void TransitionGameState(GameState newState) {
        currentGameState = newState;
        Time.timeScale = newState == GameState.UI ? 0 : 1;
    }
    
    public GameObject FindObjectInFrontOfPlayer(int mask, float distance = 0.6f) {
        raycastResults.Clear();
        findFilter.layerMask = mask;
        var direction = playerController.Direction;
        var cr = Physics2D.Raycast(playerBody.position, direction, findFilter, raycastResults, distance);

        return cr == 0 ? null : raycastResults.First().collider.gameObject;
    }

    public GameObject FindObjectInFrontAt(int mask, float distance = 0.6f) {
        var direction = playerController.Direction;
        var point = playerController.Position + direction * distance;
        var obj = Physics2D.OverlapPoint(point, mask);
        return obj == null ? null : obj.gameObject;
    }

    public static PhysicsLock AcquirePhysicsLock() {
        return new PhysicsLock(() => { ++INSTANCE.physicsLocks; }, () => { --INSTANCE.physicsLocks; });
    }

    public static bool IsPhysicsEnabled() {
        return INSTANCE.physicsLocks == 0;
    }
    
    public static async UniTask TransitionMap(Map map) {
        var _ = AcquirePhysicsLock();
        await UniTask.WaitWhile(() => INSTANCE.playerController.IsMoving);
        var loadTask = map.Scene.Load();
        loadTask.allowSceneActivation = false;
        var oldImage = GameObject.Find("SceneFadout").GetComponent<RawImage>();
        await Effect.FadeIn(0.5f, alpha => oldImage.color = oldImage.color.WithAlpha(alpha));
        loadTask.allowSceneActivation = true;
        await loadTask;
    }
}
