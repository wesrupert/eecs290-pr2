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
        StartingGame,  // TODO: Implement
        StartingLevel, // TODO: Implement
        PlayingLevel,  // TODO: Implement
        Paused,        // TODO: Implement
        LevelComplete, // TODO: Implement
        GameWin,       // TODO: Implement
        GameLose       // TODO: Implement
    };

    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState state = GameState.StartingGame;

    // Global variables for the game.
    public string level  = D_LEVEL;
    public string player = D_PLAYER;
    public float  score  = D_SCORE;
    public int    lives  = D_LIVES;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start() {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update() {
    }

    /// <summary>
    /// Creates the GUI for the game.
    ///
    /// OnGUI is called once per frame.
    /// </summary>
    void OnGUI() {
        // TODO: Add player name hook.
        GUI.Box(new Rect(0, 0, 150, 25), string.Format(F_LEVEL, player, level));
        GUI.Box(new Rect(Screen.width - 150, 0, 150, 25), string.Format(F_SCORE, lives, score));
    }

    /// <summary>
    /// Moves the game along when a level is completed.
    /// </summary>
    public void LevelComplete() {
        // TODO: Implement level switching.
    }


}
