using UnityEngine;
using System.Collections;
//ideally this will just be almost another instance of player, but with gravity removed
public class Ghost : MonoBehaviour {
		
	public float speed = 5f;
	public int jumpheight = 8;
	public int jumptime = 0;
	public bool isGrounded = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			//physics to move
			transform.Translate(Vector3.left*Time.deltaTime*speed);
		}
		if(Input.GetKey (KeyCode.RightArrow)){
			//same physics
			transform.Translate(Vector3.left*Time.deltaTime*-speed);
		}
				//jumping physics
		if(Input.GetKey (KeyCode.UpArrow) & isGrounded == true){
			rigidbody.velocity = new Vector3(0,-15,0);
		}
		if(isGrounded == false){
				jumptime++;	
		}
		//how high the player can jump
		if(jumptime>jumpheight){
			rigidbody.velocity = new Vector3(0,15,0);
		}
				//switch ghost on flip
		if(Input.GetKeyDown(KeyCode.Space)){
			Switch();
		}
	}
	
		//checks to see if collided with ground 
	//NOTE: we will need to set up the collider on the player's feet for this to work I think
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Platform") {
			isGrounded = true;
			this.rigidbody.velocity = new Vector3(0,0,0);
			jumptime = 0;
		}
	}
	
	//checks to see if jumping
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Platform") {
			isGrounded = false;
		}
	}
	
	void Die(){
		transform.position = new Vector3(0,15,0);
		//TODO: respawn at checkpoint
	}
	
	//switches ghost with player
	void Switch(){
		gameObject.AddComponent("Player");
		rigidbody.useGravity(true);
	}
}
