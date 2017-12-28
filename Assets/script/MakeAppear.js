#pragma strict

function Start () {
	showEnemy(false);
}

function Update () {

}

function OnCollisionEnter2D(){
	showEnemy(true);
}

function showEnemy(choice : boolean){
	for (var child : Transform  in transform){
		var renderer = child.gameObject.GetComponent.<SpriteRenderer>();
		if(renderer != null){
			renderer.enabled = choice;
		}


	for (var secondChild : Transform  in child){
		var renderer2 = secondChild.gameObject.GetComponent.<SpriteRenderer>();
		if(renderer2 != null){
			renderer2.enabled = choice;
		}
		}
	}
}