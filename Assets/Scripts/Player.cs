using UnityEngine;
using System.Collections;

//TODO: implement ghost
public class Player : MonoBehaviour {
	
	public float speed = 5f;
	public float jumpSpeed = 0.001f;
	public float gravity = 9.8f;
	// Use this for initialization
	void Start () {
		//nothing yet
	}
	
	// Update is called once per frame
	void Update () {
		var position = transform.position;
		if(Input.GetKey(KeyCode.LeftArrow)){
			//physics to move
			transform.Translate(Vector3.left*Time.deltaTime*speed);
		}
		if(Input.GetKey (KeyCode.RightArrow)){
			//same physics
			transform.Translate(Vector3.left*Time.deltaTime*-speed);
		}
		if(Input.GetKey (KeyCode.UpArrow)){
			//jump physics	
			//TODO: Fix jump physics so they actually work
			position.y += jumpSpeed - gravity*Time.deltaTime;
			transform.position = position;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			flip();
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

