#pragma strict


var animator : Animator;
var numberOfJump : int = 2;
var jumpVector : Vector2 = Vector2(0,350);
var speed : float = 350; 
var basicMass : float;
var basicGravityScale : float;
var plateformMassCoeff : float = 1.6;

function Start () {
	animator = gameObject.GetComponent.<Animator>();
	basicMass = GetComponent.<Rigidbody2D>().mass;
	basicGravityScale = GetComponent.<Rigidbody2D>().gravityScale;
}

function FixedUpdate(){

	if(!GameController.instance.IsGameOver){
		if(Input.GetKey(KeyCode.RightArrow)){
	    animator.ResetTrigger("Left");
		animator.SetTrigger("Right");
		transform.position =  transform.position + (Vector2.right/(Time.deltaTime*speed));
		Debug.Log(Time.deltaTime*speed);

		animator.SetTrigger("Run");	
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
		    animator.ResetTrigger("Right");
			animator.SetTrigger("Left");

			transform.position =  transform.position - (Vector2.right/(Time.deltaTime*speed));
			animator.SetTrigger("Run");	
		} else{
			animator.SetTrigger("Idle");
		}	
	}
}
function Update () {
	if(!GameController.instance.IsGameOver){
		if (Input.GetKeyDown(KeyCode.Space) && numberOfJump > 0){
			GetComponent.<Rigidbody2D>().AddForce(jumpVector,ForceMode2D.Force); 
			GetComponent.<Rigidbody2D>().velocity.y = 0;
			numberOfJump--;	
		}
	}
}	

function OnCollisionEnter2D(other : Collision2D){
	
	if(!GameController.instance.IsGameOver){
		if(other.gameObject.tag == "Ground"){
			numberOfJump = 2;
			GetComponent.<Rigidbody2D>().mass = basicMass;
			GetComponent.<Rigidbody2D>().gravityScale = basicGravityScale;
			
		}else if (other.gameObject.tag == "Enemy"){
			animator.SetTrigger("Die");
			GameController.instance.IsGameOver = true;
		}else if (other.gameObject.tag == "Plateform"){
			numberOfJump = 1;
			GetComponent.<Rigidbody2D>().mass = basicMass*plateformMassCoeff;
			GetComponent.<Rigidbody2D>().gravityScale = basicGravityScale;
		}else if (other.gameObject.tag == "PlateformHighGravity"){
			numberOfJump = 0;
			GetComponent.<Rigidbody2D>().gravityScale = GetComponent.<Rigidbody2D>().gravityScale*8;
		}
	}	
}

