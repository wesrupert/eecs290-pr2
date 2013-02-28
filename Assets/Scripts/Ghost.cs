using UnityEngine;
using System.Collections;
//Andrew Heckman

public class Ghost : MonoBehaviour {
	public Player player;

    public bool canFlip = true;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var playerPos = player.transform.position;
		var myPos = this.transform.position;
		myPos.x = playerPos.x;
		myPos.y = -playerPos.y;
		this.transform.position = myPos;
	}

	//removes player flip rights if inside object
	//don't want player to kill themselves on flip
    void OnCollisionEnter() {
        player.canFlip = false;
	}

    void OnCollisionStay() {
        player.canFlip = false;
    }
	
	//resumes flippableness after leaving object
    void OnCollisionExit() {
        player.canFlip = true;
	}
	
	
	//switches ghost with player
	void Switch(){
        player.canFlip = true;
		Vector3 position = player.transform.position;
		player.transform.position = this.transform.position;
		this.transform.position = position;
        player.rigidbody.velocity = new Vector3(
                player.rigidbody.velocity.x,
                -player.rigidbody.velocity.y,
                player.rigidbody.velocity.z);
	}
}
