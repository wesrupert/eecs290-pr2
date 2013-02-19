using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
    private const string DEFAULTLEVEL = "New level";
    private const string DEFAULTPLAYER = "Player 1"
    private const int DEFAULTLIVES = 3;

    public string level;
    public int lives;
    public float score;

	// Use this for initialization
	void Start () {
        if (level == null || level == string.Empty) {
            level = DEFAULTLEVEL;
        }

        if (lives == 0) {
            lives = DEFAULTLIVES;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        GUI.Box(new Rect(0, 0, 150, 25), "Player 1 - " + level);
        GUI.Box(new Rect(Screen.width - 150, 0, 150, 25), string.Format("{0} lives - {1}", lives, score));
    }
}
