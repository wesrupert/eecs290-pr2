using UnityEngine;
using System.Collections;
//Andrew Heckman

public class Ghost : MonoBehaviour {
	public bool canFlip = true;	
	GameObject player = GameObject.Find("Player");
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//TODO:checks position of player and mimics it invertedly (not a word)
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
	}
}