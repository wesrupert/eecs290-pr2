using UnityEngine;
using System.Collections;
//Andrew Heckman

public class Player : MonoBehaviour {
    public static string playerName = "Player 1";
    public GameManager manager;
	
	public float speed = 5f;
	public int jumpForce = 300;
	public bool canFlip = true;
	public bool isGrounded = true;
	
	// Use this for initialization
	void Start () {
		this.transform.position = manager.playerSpawn;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(manager.state == GameManager.GameState.LevelPlaying){
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
			if(Input.GetKeyDown(KeyCode.DownArrow) & canFlip){
                canFlip = false;
				flip();
			}
		}
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
	//flip mechanic that flips the camera
	//makes sure player can jump in the correct axis
	void flip(){
		GameObject camera = GameObject.Find("Camera");
		camera.SendMessage("startFlipping");
        if (jumpForce > 0 && Physics.gravity.y < 0 ||
            jumpForce < 0 && Physics.gravity.y > 0) {
            jumpForce = -jumpForce;
        }
	}
	
	//Hey Wes, I noticed you still call Die(), so I kept this here. Totally useless code.
	//Let me know if you don't need it anymore
	void Die(){
        GameObject.Find("_Game Manager").SendMessage("Died");
	}
	
	//resets the character after death
	void Respawn(Vector3 pos){
		transform.position = pos;
        canFlip = true;
        if (jumpForce < 0) {
            jumpForce = -jumpForce;
        }
	}
}

