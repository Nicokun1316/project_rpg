using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton {
    private InputController playerController;
    private GameObject player;
    private Rigidbody2D playerBody;
    private ContactFilter2D findFilter;
    private List<RaycastHit2D> raycastResults = new();
    private bool physics;
    public GameState currentGameState { get; private set; }

    public static GameManager INSTANCE;

    public static readonly int PPU = 32;
    
    public GameManager() {
        currentGameState = GameState.WORLD;
        findFilter.useLayerMask = true;
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void InitializeGame() {
        if (!PlayerPrefs.HasKey("Resolution")) {
            PlayerPrefs.SetString("Resolution", "1");
        }

        switch (PlayerPrefs.GetString("Resolution")) {
            default:
                Screen.SetResolution(640, 480, false);
                break;
            case "2":
                Screen.SetResolution(1280, 960, false);
                break;
            case "fullscreen":
                var monitor = Screen.mainWindowDisplayInfo;
                var w = monitor.width;
                var h = monitor.height;
                Screen.SetResolution(w, h, true);
                break;
        }
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
        physics = true;
    }

    protected override Singleton instance {
        get => INSTANCE;
        set => INSTANCE = (GameManager) value;
    }


    public void TransitionGameState(GameState newState) {
        currentGameState = newState;
        SetPhysicsEnabled(newState == GameState.WORLD);
    }
    
    public GameObject FindObjectInFrontOfPlayer(int mask) {
        raycastResults.Clear();
        findFilter.layerMask = mask;
        var direction = playerController.orientation switch {
            Orientation.Up => new Vector2(0, 1),
            Orientation.Down => new Vector2(0, -1),
            Orientation.Left => new Vector2(-1, 0),
            Orientation.Right => new Vector2(1, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
        var cr = Physics2D.Raycast(playerBody.position, direction, findFilter, raycastResults, 1.1f);

        return cr == 0 ? null : raycastResults.First().collider.gameObject;
    }

    public static void SetPhysicsEnabled(bool enabled) {
        INSTANCE.playerController.Mv(Vector2.zero);
        INSTANCE.physics = enabled;
    }

    public static bool IsPhysicsEnabled() {
        return INSTANCE.physics;
    }
}
