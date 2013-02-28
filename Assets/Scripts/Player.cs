using UnityEngine;
using System.Collections;
//Andrew Heckman

public class Player : MonoBehaviour {
    public string playerName = "Player 1";
	
	public float speed = 5f;
	public int jumpForce = 300;
	public bool canFlip = true;
	public bool isGrounded = true;
	public float STARTING_X = 0;
	public float STARTING_Y = 0;
	
	// Use this for initialization
	void Start () {
		var position = transform.position;
		position.x = STARTING_X;
		position.y = STARTING_Y;
		transform.position = position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if(GameManager.GameState == "LevelPlaying"){
			if(Input.GetKey(KeyCode.LeftArrow)){
				//physics to move
				transform.Translate(Vector3.left*Time.deltaTime*speed);
			}
			if(Input.GetKey (KeyCode.RightArrow)){
				//same physics
				transform.Translate(Vector3.left*Time.deltaTime*-speed);
			}
			//jump physics
			if(Input.GetKeyDown(KeyCode.UpArrow) & isGrounded){
				rigidbody.AddForce(0,jumpForce,0);
				isGrounded = false;
			}
			//flipping mechanic
			if(Input.GetKeyDown(KeyCode.Space) & canFlip){
				flip();
			}
		//}
	}
	
	void OnCollisionEnter(){
		//if(collision.gameObject.tag == "Platform"){
			isGrounded = true;
		//}
	}
	
	void OncollisionExit(){
		//if(collision.gameObject.tag == "Platform"){
			isGrounded = false;
		//}
	}
	//stop the player from flipping if flip would kill player
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
	
	//Hey Wes, I noticed you still call Die(), so I kept this here. Totally useless code.
	//Let me know if you don't need it anymore
	void Die(){
	}
	
	//resets the character after death
	void Respawn(float[] pos){
		var position = transform.position;
		position.x = pos[0];
		position.y = pos[1];
		transform.position = position;
	}
}

