using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    // Default values for the global variables.
    private const string D_LEVEL  = "New level";
    private const string D_PLAYER = "Player 1";
    private const float  D_SCORE  = 0f;
    private const int    D_LIVES  = 3;

    // Format strings for the gui.
    private const string F_LEVEL = "{0} - {1}";
    private const string F_SCORE = "{0} lives - score: {1}";

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
    }

    /// <summary>
    /// Creates the GUI for the game.
    ///
    /// OnGUI is called once per frame.
    /// </summary>
    void OnGUI() {
        switch (state) {
            case GameState.GameStarting :
                GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100),
                    "Purgatory", titleStyle);
                GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100),
                        "The fight for equilibrium", subtitleStyle);
                break;
            case GameState.GameWon :
                // TODO: Create GUI for GameWon.
                break;
            case GameState.GameLost :
                // TODO: Create GUI for GameLost.
                break;
            case GameState.LevelStarting :
                // TODO: Create GUI for LevelStarting.
                break;
            case GameState.LevelPlaying :
                GUI.Box(new Rect(0, 0, 150, 25), // Player - Level
                        string.Format(F_LEVEL, player, level), levelStyle);
                GUI.Box(new Rect(Screen.width - 150, 0, 150, 25), // Lives lives - Score
                        string.Format(F_SCORE, lives, score), scoreStyle);
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
        // TODO: Add player name hook.
    }

    /// <summary>
    /// Moves the game along when a level is completed.
    /// </summary>
    public void LevelComplete() {
        // TODO: Implement level switching.
    }
}
