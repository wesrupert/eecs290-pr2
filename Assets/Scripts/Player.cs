using UnityEngine;
using System.Collections;
//Andrew Heckman
//TODO: implement ghost
public class Player : MonoBehaviour {
	
	public float speed = 5f;
	public int jumptime = 0;
	public bool isGrounded = true;
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
		//jumping physics
		//NOTE: Right now they don't work merely because we don't have a platform. I think it'll work once we do. 
		if(Input.GetKey (KeyCode.UpArrow) & isGrounded == true){
			rigidbody.velocity = new Vector3(0,15,0);
			jumptime++;
		}
		//how high the player can jump
		if(jumptime>8){
			rigidbody.velocity = new Vector3(0,-20,0);
		}
		//flipping mechanic
		if(Input.GetKeyDown(KeyCode.Space)){
			flip();
		}
	}
	
	//checks to see if collided with ground 
	//NOTE: we will need to set up the collider on the player's feet for this to work I think
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.name == "Floor" | collision.gameObject.name == "Platform"){
			isGrounded = true;
			jumptime = 0;
		}
	}
	
	//checks to see if jumping
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.name == "Floor" | collision.gameObject.name == "Platform"){
			isGrounded = false;
		}
	}
	//flip mechanic that flips the camera
	void flip(){
		GameObject camera = GameObject.Find("Camera");
		camera.SendMessage("startFlipping");
	}
	
	//resets the character after death
	void Die(){
		transform.position = Vector3.zero;
		//TODO: respawn at checkpoint
	}
}

