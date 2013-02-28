/// Wes Rupert - wkr3
/// EECS 290   - Project 02
/// Purgatory  - GoalZone.cs
/// Script to control the goal detection and respose.

using UnityEngine;
using System.Collections;

public class GoalZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            GameObject.Find("_Game Manager").SendMessage("Goal");
        }
    }
}
