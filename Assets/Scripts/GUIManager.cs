using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
    private const string D_LEVEL = "New Level";
    private const string D_PLAYER = "Player 1";
    private const int D_SCORE = 0;
    private const int D_LIVES = 3;
    
    private const string F_LEVELTEXT = "{0} - {1}";
    private const string F_SCORE = "{0} lives - {1}";

    public string level = D_LEVEL;
    public string player = D_PLAYER;
    public int score = D_SCORE;
    public int lives = D_LIVES;

    public GUIText levelText;
    public GUIText scoreText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = string.Format(F_LEVELTEXT, player, level);
        scoreText.text = string.Format(F_SCORE, lives, score);
	}
}
