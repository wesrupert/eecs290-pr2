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
    public float GRAVITY = 15f;

    // Default values for the global variables.
    private const string D_LEVEL  = "New level";
    private const float  D_SCORE  = 0f;
    private const int    D_LIVES  = 3;
    private const int    D_SWIDTH = 200;
    private const float  D_TIMESPEED = 2f;

    // Constant strings for the gui.
    private const string S_COUNTDOWN = "Start in {0}";
    private const string S_LEVEL = "{0} - {1}";
    private const string S_LIVES = "{0} lives left";
    private const string S_ONELIFE = "1 life left";
    private const string S_PAUSED = "Paused.";
    private const string S_SCORE = "{0} lives - score: {1:N2}";

    // Constant values for splash screens.
    private const float N_SPLASH_W      = 1280, N_SPLASH_H      = 960;
    private const float N_TITLE_START_X = 333,  N_TITLE_START_Y = 453;
    private const float N_TITLE_START_W = 595,  N_TITLE_START_H = 132;
    private const float N_NEWGAME_X     = 419,  N_NEWGAME_Y     = 451;
    private const float N_NEWGAME_W     = 479,  N_NEWGAME_H     = 130;
    private const float N_DIED_GO_X     = 134,  N_DIED_GO_Y     = 521;
    private const float N_DIED_GO_W     = 360,  N_DIED_GO_H     = 110;
    private const float N_DIED_QUIT_X   = 702,  N_DIED_QUIT_Y   = 524;
    private const float N_DIED_QUIT_W   = 357,  N_DIED_QUIT_H   = 110;

    // Length of a countdown.
    private const float N_COUNTDOWN = 3f;

    // GUI boxes for displaying text at various points in the game.
    // Implemented as properties since they update in real time but are used as constants.
    private Rect G_TITLE {
        get {
            return new Rect(
                    0,
                    0,
                    Screen.width,
                    Screen.height);
        }
    }
    private Rect G_CENTER_BIG {
        get {
            return new Rect(
                    Screen.width / 2 - 150,
                    Screen.height / 2 - 50,
                    300,
                    100);
        }
    }
    private Rect G_TOP_SMALL_LEFT {
        get { return new Rect(0, 0, swidth, 25); }
    }
    private Rect G_TOP_SMALL_RIGHT {
        get { return new Rect(Screen.width - swidth, 0, swidth, 25); }
    }
    private Rect G_LIVESLEFT {
        get {
            float screenX = (Screen.width * (N_DIED_GO_X + N_DIED_GO_W) / N_SPLASH_W);
            float screenY = Screen.height * N_DIED_GO_Y / N_SPLASH_H;
            float screenW = (Screen.width * N_DIED_QUIT_X / N_SPLASH_W) - screenX;
            float screenH = (Screen.height * (N_DIED_QUIT_Y + N_DIED_QUIT_H) / N_SPLASH_H) - screenY;

            return new Rect(screenX, screenY, screenW, screenH);
        }
    }

    // GUI boxes for placing buttons over the splash screens.
    private Rect B_TITLE_START {
        get {
            float screenX = Screen.width * N_TITLE_START_X / N_SPLASH_W;
            float screenY = Screen.height * N_TITLE_START_Y / N_SPLASH_H;
            float screenW = (Screen.width * (N_TITLE_START_X + N_TITLE_START_W) / N_SPLASH_W) - screenX;
            float screenH = (Screen.height * (N_TITLE_START_Y + N_TITLE_START_H) / N_SPLASH_H) - screenY;

            return new Rect(screenX, screenY, screenW, screenH);
        }
    }
    private Rect B_NEWGAME {
        get {
            float screenX = Screen.width * N_NEWGAME_X / N_SPLASH_W;
            float screenY = Screen.height * N_NEWGAME_Y / N_SPLASH_H;
            float screenW = (Screen.width * (N_NEWGAME_X + N_NEWGAME_W) / N_SPLASH_W) - screenX;
            float screenH = (Screen.height * (N_NEWGAME_Y + N_NEWGAME_H) / N_SPLASH_H) - screenY;

            return new Rect(screenX, screenY, screenW, screenH);
        }
    }
    private Rect B_DIED_GO {
        get {
            float screenX = Screen.width * N_DIED_GO_X / N_SPLASH_W;
            float screenY = Screen.height * N_DIED_GO_Y / N_SPLASH_H;
            float screenW = (Screen.width * (N_DIED_GO_X + N_DIED_GO_W) / N_SPLASH_W) - screenX;
            float screenH = (Screen.height * (N_DIED_GO_Y + N_DIED_GO_H) / N_SPLASH_H) - screenY;

            return new Rect(screenX, screenY, screenW, screenH);
        }
    }
    private Rect B_DIED_QUIT {
        get {
            float screenX = Screen.width * N_DIED_QUIT_X / N_SPLASH_W;
            float screenY = Screen.height * N_DIED_QUIT_Y / N_SPLASH_H;
            float screenW = (Screen.width * (N_DIED_QUIT_X + N_DIED_QUIT_W) / N_SPLASH_W) - screenX;
            float screenH = (Screen.height * (N_DIED_QUIT_Y + N_DIED_QUIT_H) / N_SPLASH_H) - screenY;

            return new Rect(screenX, screenY, screenW, screenH);
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
    public float  timespeed = D_TIMESPEED;
    public int    lives  = D_LIVES;
    public int    swidth = D_SWIDTH;
    public Vector3 playerSpawn = Vector3.zero;

    // Styles for various GUIs
    public GUIStyle levelStyle, scoreStyle, livesLeftStyle, countdownStyle, buttonStyle;
    public GUIStyle titleSplash, winSplash, dieSplash, dieSplashNoRetry, levelSplash;

    private float startTime, timeScale;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start() {
        Time.timeScale = timespeed;
        startTime = Time.realtimeSinceStartup;
        Physics.gravity = Vector3.down * GRAVITY;
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
                if (startTime + N_COUNTDOWN - Time.realtimeSinceStartup < 0) {
                    Application.LoadLevel(nextLevel);
                }
                break;
            default :
                break;
        }
    }

    /// <summary>
    /// Creates the GUI for the game. OnGUI is called once per frame.
    /// </summary>
    void OnGUI() {
        switch (state) {
            case GameState.GameStarting :
                GUISplashScreen(titleSplash);
                if (GUI.Button(B_TITLE_START, string.Empty, buttonStyle)) {
                    Application.LoadLevel(nextLevel);
                }
                break;
            case GameState.GameWon :
                GUISplashScreen(winSplash);
                if (GUI.Button(B_NEWGAME, string.Empty, buttonStyle)) {
                    Application.LoadLevel(STARTSCREEN);
                }
                break;
            case GameState.GameLost :
                if (lives > 0) {
                    GUISplashScreen(dieSplash);
                    if (lives == 1) {
                        GUI.Box(G_LIVESLEFT, S_ONELIFE, livesLeftStyle);
                    }
                    else {
                        GUI.Box(G_LIVESLEFT, string.Format(S_LIVES, lives), livesLeftStyle);
                    }

                    if (GUI.Button(B_DIED_GO, string.Empty, buttonStyle)) {
                        lives--;
                        player.SendMessage("Respawn", playerSpawn);
                        state = GameState.LevelStarting;
                    }
                    if (GUI.Button(B_DIED_QUIT, string.Empty, buttonStyle)) {
                        Application.LoadLevel(STARTSCREEN);
                    }
                }
                else {
                    GUISplashScreen(dieSplashNoRetry);
                    if (GUI.Button(B_NEWGAME, string.Empty, buttonStyle)) {
                        Application.LoadLevel(STARTSCREEN);
                    }
                }
                break;
            case GameState.LevelStarting :
                int countdown = (int)(Mathf.Ceil(startTime + N_COUNTDOWN - Time.realtimeSinceStartup));
                if (countdown > 0) {
                    GUI.Box(G_CENTER_BIG, string.Format(S_COUNTDOWN, countdown), countdownStyle);
                }
                break;
            case GameState.LevelPlaying :
                GUI.Box(G_TOP_SMALL_LEFT, string.Format(S_LEVEL, player.playerName, level), levelStyle);
                GUI.Box(G_TOP_SMALL_RIGHT, string.Format(S_SCORE, lives, score), scoreStyle);
                break;
            case GameState.LevelPaused :
                GUI.Box(G_CENTER_BIG, S_PAUSED, countdownStyle);
                break;
            case GameState.LevelCompleted :
                GUISplashScreen(levelSplash);
                break;
            default :
                // Nothing is drawn when we don't know what's going on.
                break;
        }
    }

    /// <summary>
    /// Sets up level complete screen.
    /// </summary>
    public void Goal() {
        startTime = Time.realtimeSinceStartup;
        state = GameState.LevelCompleted;
    }

    /// <summary>
    /// Sets up the game over screen.
    /// </summary>
    public void Died() {
        state = GameState.GameLost;
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    private void pause() {
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
        state = GameState.LevelPaused;
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    private void unpause() {
        Time.timeScale = timeScale;
        state = GameState.LevelPlaying;
    }

    /// <summary>
    /// Displays a fullscreen splashscreen using the given style.
    /// </summary>
    private void GUISplashScreen(GUIStyle screen) {
        GUI.Box(
            new Rect(0, 0, Screen.width, Screen.height),
            string.Empty,
            screen);
    }
}
