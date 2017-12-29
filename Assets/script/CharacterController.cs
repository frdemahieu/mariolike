using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	Animator animator; 
	int numberOfJump = 2;
	Vector2 jumpVector = new Vector2(0,350);
	float speed = 350f; 
	float basicMass ;
	float basicGravityScale;
	float plateformMassCoeff = 1.6f;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		animator = GetComponent <Animator> ();
		rigidBody = GetComponent <Rigidbody2D> ();
		basicMass =rigidBody.mass;
		basicGravityScale = rigidBody.gravityScale;
	}

	void FixedUpdate(){

		if (!GameController.instance.gameOver) {
			if(Input.GetKey(KeyCode.RightArrow)){
				animator.ResetTrigger("Left");
				animator.SetTrigger("Right");
				transform.position =  (Vector2) transform.position + (Vector2.right/(Time.deltaTime*speed));


				animator.SetTrigger("Run");	
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				animator.ResetTrigger("Right");
				animator.SetTrigger("Left");

				transform.position =  (Vector2) transform.position - (Vector2.right/(Time.deltaTime*speed));
				animator.SetTrigger("Run");	
			} else{
				animator.SetTrigger("Idle");
			}	
		}
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && numberOfJump > 0){
			rigidBody.AddForce(jumpVector,ForceMode2D.Force); 
			rigidBody.velocity = new Vector2(rigidBody.velocity.x,0);
			numberOfJump--;	
		}
	}

	void OnCollisionEnter2D(Collision2D other){

		if(!GameController.instance.gameOver){
			if(other.gameObject.tag.Equals ("Ground")){
				numberOfJump = 2;
				rigidBody.mass = basicMass;
				rigidBody.gravityScale = basicGravityScale;

			}else if (other.gameObject.tag.Equals ("Enemy")){
				animator.SetTrigger("Die");
				GameController.instance.gameOver = true;
			}else if (other.gameObject.tag.Equals ("Plateform") || other.gameObject.tag.Equals ("Tube")){
				numberOfJump = 1;
				rigidBody.mass = basicMass*plateformMassCoeff;
				rigidBody.gravityScale = basicGravityScale;
			}else if (other.gameObject.tag.Equals ("PlateformHighGravity")){
				numberOfJump = 0;
				rigidBody.gravityScale = rigidBody.gravityScale*8;
			}
		}	
	}
}
