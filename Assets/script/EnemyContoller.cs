using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour {

	bool isGoingLeft = false;
	Transform transform; 
	float speed = 500.0f; 
	Animator animator;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		animator = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!GameController.instance.gameOver) {
				
			if (!isGoingLeft) {
				transform.position = (Vector2)transform.position + (Vector2.right / (Time.deltaTime * speed));
				animator.ResetTrigger ("Left");
				animator.SetTrigger ("Right");
		
			} else {
				transform.position = (Vector2)transform.position - (Vector2.right / (Time.deltaTime * speed));
				animator.ResetTrigger ("Right");
				animator.SetTrigger ("Left");
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (!GameController.instance.gameOver) {
			isGoingLeft = isGoingLeft ? false : true;
		}
	}	
}
