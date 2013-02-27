using UnityEngine;
using System.Collections;
//Andrew Heckman

public class Player : MonoBehaviour {
    public string playerName = "Player 1";
	
	public float speed = 5f;
	public int jumpForce = 300;
	public bool canFlip = true;
	// Use this for initialization
	void Start () {
		//nothing yet
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			//physics to move
			transform.Translate(Vector3.left*Time.deltaTime*speed);
		}
		if(Input.GetKey (KeyCode.RightArrow)){
			//same physics
			transform.Translate(Vector3.left*Time.deltaTime*-speed);
		}
		//jump physics
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			rigidbody.AddForce(0,jumpForce,0);
		}
		//flipping mechanic
		if(Input.GetKeyDown(KeyCode.Space) & canFlip){
			flip();
		}
	}
	
	void flippableInvert(bool status){
		canFlip = status;
	}
	//flip mechanic that flips the camera
	//makes sure player can jump in the correct axis
	void flip(){
		GameObject camera = GameObject.Find("Camera");
		camera.SendMessage("startFlipping");
		jumpForce = -jumpForce;
	}
	
	//resets the character after death
	void Die(){
		transform.position = Vector3.zero;
		//TODO: respawn at checkpoint
	}
}

