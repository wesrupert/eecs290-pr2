using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    // Game-wide properties.
    public const string GAMETITLE = "Purgatory";
    public const string GAMESUBTITLE = "The fight for equilibrium";

    // Default values for the global variables.
    private const string D_LEVEL  = "New level";
    private const string D_PLAYER = "Player 1";
    private const float  D_SCORE  = 0f;
    private const int    D_LIVES  = 3;

    // Constant strings for the gui.
    private const string S_COUNTDOWN = "Start in {0}";
    private const string S_GAMEOVER = "No lives - Game Over!";
    private const string S_GO = "GO!";
    private const string S_LEVEL = "{0} - {1}";
    private const string S_LIVES = "{0} lives left";
    private const string S_LOSE = "You died!";
    private const string S_PLAYAGAIN = "Again!";
    private const string S_QUIT = "QUIT";
    private const string S_SCORE = "{0} lives - score: {1:N2}";
    private const string S_STARTGAME = "PLAY";
    private const string S_TRYAGAIN = "GO";
    private const string S_WIN = "You won!";

    // GUI boxes for displaying text at various points in the game.
    // Implemented as properties since they update in real time but are used as constants.
    private Rect G_CENTER_BIG {
        get {
            return new Rect(
                    Screen.width / 2 - 150,
                    Screen.height / 2 - 50,
                    300,
                    100);
        }
    }
    private Rect G_LEFT_BUTTON {
        get {
            return new Rect(
                    Screen.width / 2 - 200,
                    Screen.height / 2 + 60,
                    80,
                    25);
        }
    }
    private Rect G_RIGHT_BUTTON {
        get {
            return new Rect(
                    Screen.width / 2 + 200,
                    Screen.height / 2 + 60,
                    80,
                    25);
        }
    }
    private Rect G_CENTER_BUTTON {
        get {
            return new Rect(
                    Screen.width / 2 - 40,
                    Screen.height / 2 + 60,
                    80,
                    25);
        }
    }
    private Rect G_TOP_SMALL {
        get {
            return new Rect(0, 0, Screen.width, 25);
        }
    }

    /// <summary>
    /// Enumeration of possible states for the game.
    /// </summary>
    public enum GameState {
        GameStarting,  // TODO: Implement.
        GameWon,       // TODO: Implement.
        GameLost,      // TODO: Implement.
        LevelStarting, // TODO: Implement.
        LevelPlaying,  // TODO: Implement.
        LevelPaused,   // TODO: Implement.
        LevelCompleted // TODO: Implement.
    };

    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState state = GameState.GameStarting;

    // Global variables for the game.
    public string level  = D_LEVEL;
    public string player = D_PLAYER;
    public float  score  = D_SCORE;
    public int    lives  = D_LIVES;

    // Styles for various GUIs
    public GUIStyle titleStyle, subtitleStyle;
    public GUIStyle levelStyle, scoreStyle;

    private float startTime;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start() {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update() {
        // Update the score.
        score += Time.deltaTime;

        switch (state) {
            case GameStarting :
                break;
            case GameWon :
                break;
            case GameLost :
                break;
            case LevelStarting :
                if (startTime - Time.realtimeSinceStartup) {
                    state = GameState.LevelPlaying;
                }
                break;
            case LevelPlaying :
                break;
            case LevelPaused :
                break;
            case LevelCompleted :
                break;
        }
    }

    /// <summary>
    /// Creates the GUI for the game.
    ///
    /// OnGUI is called once per frame.
    /// </summary>
    void OnGUI() {
        switch (state) {
            case GameState.GameStarting :
                GUI.Box(G_CENTER_BIG, GAMETITLE, titleStyle);
                GUI.Box(G_CENTER_BIG, GAMESUBTITLE, subtitleStyle);
                if (GUI.Button(G_CENTER_BUTTON, S_STARTGAME)) {
                    startTime = Time.realtimeSinceStartup;
                    // TODO: Implement play game.
                }
                break;
            case GameState.GameWon :
                GUI.Box(G_CENTER_BIG, S_WIN, titleStyle);
                if (GUI.Button(G_CENTER_BUTTON, S_PLAYAGAIN)) {
                    // TODO: Implement play again.
                }
                break;
            case GameState.GameLost :
                GUI.Box(G_CENTER_BIG, S_LOSE, titleStyle);
                if (lives >= 0) {
                    GUI.Box(G_CENTER_BIG, string.Format(S_LIVES, lives));
                    if (GUI.Button(G_LEFT_BUTTON, S_TRYAGAIN)) {
                        // TODO: Implement respawn.
                    }
                    if (GUI.Button(G_RIGHT_BUTTON, S_QUIT)) {
                        // TODO: Implement quit.
                    }
                }
                else {
                    GUI.Box(G_CENTER_BIG, S_GAMEOVER, subtitleStyle);
                    if (GUI.Button(G_CENTER_BUTTON, S_QUIT)) {
                        // TODO: Implement quit.
                    }
                }
                break;
            case GameState.LevelStarting :
                int countdown = (int)(Mathf.Ceil(startTime - Time.realtimeSinceStartup));
                if (countdown > 0) {
                    GUI.Box(G_CENTER_BIG, string.Format(S_COUNTDOWN, countdown), titleStyle);
                }
                else {
                    GUI.Box(G_CENTER_BIG, S_GO, titleStyle);
                }
                break;
            case GameState.LevelPlaying :
                GUI.Box(G_TOP_SMALL, string.Format(S_LEVEL, player, level), levelStyle);
                GUI.Box(G_TOP_SMALL, string.Format(S_SCORE, lives, score), scoreStyle);
                break;
            case GameState.LevelPaused :
                // TODO: Create GUI for LevelPaused.
                break;
            case GameState.LevelCompleted :
                // TODO: Create GUI for LevelCompleted.
                break;
            default :
                // Nothing is drawn when we don't know what's going on.
                break;
        }
    }

    /// <summary>
    /// Moves the game along when a level is completed.
    /// </summary>
    public void LevelComplete() {
        // TODO: Implement level switching.
    }
}
