using UnityEngine;
using System.Collections;
//Andrew Heckman

public class Ghost : MonoBehaviour {
	public bool canFlip = true;	
	GameObject player;
    
	// Use this for initialization
	void Start () {
		 player = GameObject.Find("Player");
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
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag != "Platform") {
			canFlip = false;
			player.SendMessage("flippableInvert", canFlip);
		}
	}
	
	//resumes flippableness after leaving object
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag != "Platform") {
			canFlip = true;
			player.SendMessage("flippableInvert", canFlip);
		}
	}
	
	
	//switches ghost with player
	void Switch(){
		var position = transform.position;
		GameObject player = GameObject.Find("Player");
		position = player.transform.position;
		player.transform.position = this.transform.position;
		transform.position = position;
        player.rigidbody.velocity = new Vector3(
                player.rigidbody.velocity.x,
                player.rigidbody.velocity.y,
                player.rigidbody.velocity.z);
	}
}
