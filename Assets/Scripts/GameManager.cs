/// Wes Rupert - wkr3
/// EECS 290   - Project 02
/// Purgatory  - GameManager.cs
/// Script to control the environment of the game.

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    // Game-wide properties.
    public const string GAMETITLE = "Purgatory";
    public const string GAMESUBTITLE = "The fight for equilibrium";
    public const string STARTSCREEN = "title";

    // Default values for the global variables.
    private const string D_LEVEL  = "New level";
    private const float  D_SCORE  = 0f;
    private const int    D_LIVES  = 3;

    // Constant strings for the gui.
    private const string S_COUNTDOWN = "Start in {0}";
    private const string S_GAMEOVER = "No lives - Game Over!";
    private const string S_GO = "GO!";
    private const string S_LEVEL = "{0} - {1}";
    private const string S_LIVES = "{0} lives left";
    private const string S_LOSE = "You died!";
    private const string S_NEXTLEVEL = "Proceeding to next level...";
    private const string S_PAUSED = "Paused.";
    private const string S_PLAYAGAIN = "Again!";
    private const string S_QUIT = "QUIT";
    private const string S_SCORE = "{0} lives - score: {1:N2}";
    private const string S_STARTGAME = "PLAY";
    private const string S_TRYAGAIN = "GO";
    private const string S_WIN = "You won!";
    private const string S_WINLEVEL = "Level complete!";

    // Length of a countdown.
    private const float N_COUNTDOWN = 3f;

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
    private Rect G_TOP_SMALL {
        get {
            return new Rect(0, 0, Screen.width, 25);
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

    /// <summary>
    /// Enumeration of possible states for the game.
    /// </summary>
    public enum GameState {
        GameStarting,
        GameWon,
        GameLost,
        LevelStarting,
        LevelPlaying,
        LevelPaused,
        LevelCompleted
    };

    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState state = GameState.GameStarting;

    // Global variables for the game.
    public Player player;
    public string nextLevel;
    public string level  = D_LEVEL;
    public float  score  = D_SCORE;
    public int    lives  = D_LIVES;

    // Styles for various GUIs
    public GUIStyle titleStyle, subtitleStyle;
    public GUIStyle levelStyle, scoreStyle;

    private float startTime, timeScale;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start() {
        startTime = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update() {
        switch (state) {
            case GameState.GameStarting :
                break;
            case GameState.GameWon :
                break;
            case GameState.GameLost :
                break;
            case GameState.LevelStarting :
                if (startTime + N_COUNTDOWN - Time.realtimeSinceStartup < 0) {
                    state = GameState.LevelPlaying;
                }
                break;
            case GameState.LevelPlaying :
                score += Time.deltaTime;
                if (Input.GetKey(KeyCode.P)) {
                    pause();
                }
                break;
            case GameState.LevelPaused :
                if (Input.GetKey(KeyCode.P)) {
                    unpause();
                }
                break;
            case GameState.LevelCompleted :
                break;
            default :
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
                    Application.LoadLevel(nextLevel);
                }
                break;
            case GameState.GameWon :
                GUI.Box(G_CENTER_BIG, S_WIN, titleStyle);
                if (GUI.Button(G_CENTER_BUTTON, S_PLAYAGAIN)) {
                    Application.LoadLevel(STARTSCREEN);
                }
                break;
            case GameState.GameLost :
                GUI.Box(G_CENTER_BIG, S_LOSE, titleStyle);
                if (lives >= 0) {
                    GUI.Box(G_CENTER_BIG, string.Format(S_LIVES, lives));
                    if (GUI.Button(G_LEFT_BUTTON, S_TRYAGAIN)) {
                        lives--;
                        player.SendMessage("Respawn");
                        state = GameState.LevelStarting;
                    }
                    if (GUI.Button(G_RIGHT_BUTTON, S_QUIT)) {
                        Application.LoadLevel(STARTSCREEN);
                    }
                }
                else {
                    GUI.Box(G_CENTER_BIG, S_GAMEOVER, subtitleStyle);
                    if (GUI.Button(G_CENTER_BUTTON, S_QUIT)) {
                        Application.LoadLevel(STARTSCREEN);
                    }
                }
                break;
            case GameState.LevelStarting :
                int countdown = (int)(Mathf.Ceil(startTime + N_COUNTDOWN - Time.realtimeSinceStartup));
                if (countdown > 0) {
                    GUI.Box(G_CENTER_BIG, string.Format(S_COUNTDOWN, countdown), titleStyle);
                }
                else {
                    GUI.Box(G_CENTER_BIG, S_GO, titleStyle);
                }
                break;
            case GameState.LevelPlaying :
                GUI.Box(G_TOP_SMALL, string.Format(S_LEVEL, player.playerName, level), levelStyle);
                GUI.Box(G_TOP_SMALL, string.Format(S_SCORE, lives, score), scoreStyle);
                break;
            case GameState.LevelPaused :
                GUI.Box(G_CENTER_BIG, S_PAUSED, subtitleStyle);
                break;
            case GameState.LevelCompleted :
                GUI.Box(G_CENTER_BIG, S_WINLEVEL, titleStyle);
                GUI.Box(G_CENTER_BIG, S_NEXTLEVEL, subtitleStyle);
                break;
            default :
                // Nothing is drawn when we don't know what's going on.
                break;
        }
    }

    private void pause() {
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
        state = GameState.LevelPaused;
    }

    private void unpause() {
        Time.timeScale = timeScale;
        state = GameState.LevelPlaying;
    }
}
